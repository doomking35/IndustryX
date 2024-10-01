using IndustryX.ServiceUser.DAL;
using IndustryX.ServiceUser.Middlewares;
using IndustryX.ServiceUser.Repositories;
using IndustryX.ServiceUser.Repositories.Interfaces;
using IndustryX.ServiceUser.Sagas;
using IndustryX.ServiceUser.Service;
using IndustryX.ServiceUser.Services.Interfaces;
using MassTransit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB:User"));

// MongoClient baðlantýsýný ayarlara göre yapýlandýr
builder.Services.AddSingleton<IMongoClient>(s =>
{
    var mongoSettings = s.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(mongoSettings.ConnectionURI);
});

// Dependency Injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddMassTransit(x =>
{
    // Add all consumers in the assembly
    x.AddConsumers(Assembly.GetExecutingAssembly());

    // Add the saga state machine
    x.AddSagaStateMachine<UserCreationSaga, UserCreationSagaState>().MongoDbRepository(cfg =>
    {
        //var mongoSettings = .GetRequiredService<IOptions<MongoDBSettings>>().Value;
        cfg.Connection = "mongodb+srv://suatalkan:WfZsSIdPqrBsmDs6@cluster0.o6uos.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
        cfg.DatabaseName = "USERDB";
        cfg.CollectionName = "users";
    });
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);

        // No explicit receive endpoint here, since this is the producer
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseCheckUniqueUsernameMiddleware();

app.Run();
