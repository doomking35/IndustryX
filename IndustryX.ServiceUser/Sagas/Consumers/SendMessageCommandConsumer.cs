using IndustryX.InfrastructureModels.Models;
using MassTransit;

namespace IndustryX.ServiceUser.Sagas.Consumers
{
    public class SendMessageCommandConsumer(IPublishEndpoint publishEndpoint) : ConsumerBase<SendMessageCommand>
    {
        protected override async Task ConsumeInternal(ConsumeContext<SendMessageCommand> context)
        {
            var message = context.Message;
            await publishEndpoint.Publish(new MessageBridgeSendMessageRequest()
            { 
                MessageType = InfrastructureModels.Enums.MessageType.Email,
                Subject = "TEST",
                To = message.User.Email,
                CorrelationId = message.CorrelationId,
            });
        }
    }
}
