using IndustryX.InfrastructureModels.Models;
using MassTransit;

namespace IndustryX.ServiceUser.Sagas.Consumers
{
    public class MessageSentConsumer : ConsumerBase<MessageSent>
    {
        protected override Task ConsumeInternal(ConsumeContext<MessageSent> context)
        {
            return Task.CompletedTask;
        }
    }
}
