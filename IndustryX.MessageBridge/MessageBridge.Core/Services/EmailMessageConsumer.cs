using IndustryX.InfrastructureModels.Models;
using IndustryX.MessageBridge.MessageBridge.Models;
using MassTransit;
using System.Net.Mail;

namespace IndustryX.MessageBridge.MessageBridge.Core.Services
{
    public class EmailMessageConsumer : IConsumer<MessageBridgeSendMessageRequest>
    {
        private readonly MessageService _messageService;

        public EmailMessageConsumer(MessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task Consume(ConsumeContext<MessageBridgeSendMessageRequest> context)
        {
            var massage = context.Message;
            _messageService.QueueMessage(massage);
            await Task.CompletedTask;
        }
    }
}
