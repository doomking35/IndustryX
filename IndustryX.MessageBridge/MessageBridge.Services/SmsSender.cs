using IndustryX.MessageBridge.MessageBridge.Core.Interfaces;
using IndustryX.MessageBridge.MessageBridge.Models;

namespace IndustryX.MessageBridge.MessageBridge.Services
{
    public class SmsSender : IMessageSender
    {
        //public async Task SendMessageAsync(string to, string message)
        //{
        //    // SMS gönderme işlemi
        //    Console.WriteLine($"Sending SMS to {to}: {message}");
        //    await Task.CompletedTask;
        //}

        public Task SendMessageAsync(string to, string subject, string templateName, Dictionary<string, string> placeholders)
        {
            throw new NotImplementedException();
        }
    }
}
