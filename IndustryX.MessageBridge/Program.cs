using IndustryX.MessageBridge.MessageBridge.API;
using IndustryX.MessageBridge.MessageBridge.Core.Factories;
using IndustryX.MessageBridge.MessageBridge.Core.Interfaces;
using IndustryX.MessageBridge.MessageBridge.Core.Services;
using IndustryX.MessageBridge.MessageBridge.Models;
using IndustryX.MessageBridge.MessageBridge.Services;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
});

// MassTransit konfigürasyonu
builder.Services.AddMassTransit(x =>
{
    // Add the EmailMessageConsumer to the configuration
    x.AddConsumer<EmailMessageConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// IMessageSender implementations
builder.Services.AddTransient<SmsSender>();
builder.Services.AddTransient<EmailSender>();

// Factory and Service
builder.Services.AddSingleton<MessageSenderFactory>();
builder.Services.AddTransient<MessageService>();

// Background Queue and Hosted Service
builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
builder.Services.AddHostedService<QueuedHostedService>();

//Template Loader Service
builder.Services.Configure<TemplateSettings>(builder.Configuration.GetSection("TemplateSettings"));
builder.Services.AddSingleton<ITemplateLoader, TemplateLoader>();

// SMTP Ayarlarý için Servis Kayýtlarý
builder.Services.AddSingleton<ISmtpConfigurationProvider, SmtpConfigurationProvider>();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));



var app = builder.Build();

app.UseSerilogRequestLogging(); // Serilog middleware'i kullan
app.UseStaticFiles();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
