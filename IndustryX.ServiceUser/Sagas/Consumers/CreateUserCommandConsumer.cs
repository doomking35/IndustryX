using IndustryX.InfrastructureModels.Models;
using IndustryX.ServiceUser.Models;
using IndustryX.ServiceUser.Repositories;
using IndustryX.ServiceUser.Repositories.Interfaces;
using MassTransit;

namespace IndustryX.ServiceUser.Sagas.Consumers
{
    public class CreateUserCommandConsumer(IUserRepository userRepository, IPublishEndpoint publishEndpoint) : ConsumerBase<CreateUserCommand>
    {
        protected override async Task ConsumeInternal(ConsumeContext<CreateUserCommand> context)
        {
            var message = context.Message;
            await userRepository.CreateUserAsync(message.User);
            await publishEndpoint.Publish(new UserCreated { CorrelationId = message.CorrelationId});
        }
    }
}
