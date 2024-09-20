using IndustryX.MessageBridge.Common.Enums;
using IndustryX.MessageBridge.MessageBridge.Core.Interfaces;
using IndustryX.MessageBridge.MessageBridge.Models;
using IndustryX.MessageBridge.MessageBridge.Services;
using System.Net.Mail;

namespace IndustryX.MessageBridge.MessageBridge.Core.Factories
{
    public class MessageSenderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public MessageSenderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMessageSender CreateSender(MessageType messageType)
        {
            return messageType switch
            {
                MessageType.SMS => _serviceProvider.GetService<SmsSender>(),
                MessageType.Email => _serviceProvider.GetService<EmailSender>(),
                _ => throw new ArgumentException("Invalid message type")
            };
        }
    }
}
