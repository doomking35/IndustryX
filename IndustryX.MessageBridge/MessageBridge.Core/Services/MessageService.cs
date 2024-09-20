using IndustryX.MessageBridge.Common.Enums;
using IndustryX.MessageBridge.MessageBridge.Core.Factories;
using IndustryX.MessageBridge.MessageBridge.Core.Interfaces;

namespace IndustryX.MessageBridge.MessageBridge.Core.Services
{
    public class MessageService
    {
        private readonly MessageSenderFactory _messageSenderFactory;
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger<MessageService> _logger; // Loglama için ILogger eklendi

        public MessageService(MessageSenderFactory messageSenderFactory, IBackgroundTaskQueue taskQueue, ILogger<MessageService> logger)
        {
            _messageSenderFactory = messageSenderFactory;
            _taskQueue = taskQueue;
            _logger = logger;
        }

        public void QueueMessage(MessageType messageType, string to, string subject, string templateName, Dictionary<string, string> placeholders)
        {
            _logger.LogInformation("Queuing {MessageType} message to {Recipient}", messageType, to);

            // Queue'ya mesaj gönderimi için bir iş ekleniyor
            _taskQueue.QueueMessage(async cancellationToken =>
            {
                try
                {
                    var sender = _messageSenderFactory.CreateSender(messageType);
                    await sender.SendMessageAsync(to, subject, templateName, placeholders);
                    _logger.LogInformation("{MessageType} message to {Recipient} sent successfully", messageType, to);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error sending {MessageType} message to {Recipient}", messageType, to);
                }               
            });
        }
    }

}
