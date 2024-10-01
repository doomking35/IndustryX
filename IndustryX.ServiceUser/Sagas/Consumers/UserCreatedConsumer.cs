using MassTransit;

namespace IndustryX.ServiceUser.Sagas.Consumers
{
    public class UserCreatedConsumer : ConsumerBase<UserCreated>
    {
        protected override Task ConsumeInternal(ConsumeContext<UserCreated> context)
        {
            return Task.CompletedTask;
        }
    }
}
