using IndustryX.MessageBridge.MessageBridge.Core.Interfaces;
using IndustryX.MessageBridge.MessageBridge.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using MassTransit;
using IndustryX.InfrastructureModels.Models;

namespace IndustryX.MessageBridge.MessageBridge.Services
{
    public class EmailSender : IMessageSender
    {
        private readonly ITemplateLoader _templateLoader;
        private readonly SmtpSettings _smtpSettings;
        private readonly IBus _bus;
        public EmailSender(ISmtpConfigurationProvider smtpConfigurationProvider, ITemplateLoader templateLoader, IBus bus)
        {
            _smtpSettings = smtpConfigurationProvider.GetSmtpSettings();
            _templateLoader = templateLoader;
            _bus = bus;
        }
        public async Task SendMessageAsync(string to, string subject, string templateName, Dictionary<string, string> placeholders, Guid correlationId)
        {
            using (var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
            {
                client.EnableSsl = _smtpSettings.EnableSsl;
                client.Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.Sender),
                    Subject = subject,
                    Body = await _templateLoader.LoadTemplateAsync(templateName, placeholders),
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                await client.SendMailAsync(mailMessage);
                await _bus.Publish(new MessageSent { CorrelationId = correlationId});
                await Task.CompletedTask;
            }
        }
    }
}
