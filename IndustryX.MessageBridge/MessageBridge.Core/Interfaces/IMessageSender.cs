using IndustryX.MessageBridge.MessageBridge.Models;

namespace IndustryX.MessageBridge.MessageBridge.Core.Interfaces
{
    public interface IMessageSender
    {
        Task SendMessageAsync(string to, string subject, string templateName, Dictionary<string, string> placeholders);
    }
}
