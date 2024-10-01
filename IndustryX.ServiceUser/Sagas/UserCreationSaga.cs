using MassTransit.Transports.Fabric;
using MassTransit;
using IndustryX.InfrastructureModels.Models;
using IndustryX.ServiceUser.Models;
using IndustryX.InfrastructureModels.Interfaces;

namespace IndustryX.ServiceUser.Sagas
{
    public class UserCreationSaga :
        MassTransitStateMachine<UserCreationSagaState>
    {
        public UserCreationSaga()
        {
            InstanceState(x => x.CurrentState);

            Event(() => UserCreationInitiated, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => UserCreated, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => MessageSent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => UserCreationFailed, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => MessageSendingFailed, x => x.CorrelateById(context => context.Message.CorrelationId));

            Initially(
                When(UserCreationInitiated)
                    .Then(context =>
                    {
                        context.Saga.User = context.Message.User;
                    })
                    .TransitionTo(UserCreationRequested)
                    .Publish(context => new CreateUserCommand
                    {
                        CorrelationId = context.Saga.CorrelationId,
                        User = context.Saga.User,
                    })
            );

            During(UserCreationRequested,
                When(UserCreated)
                    .TransitionTo(MessageSendingRequested)
                    .Publish(context => new SendMessageCommand
                    {
                        CorrelationId = context.Saga.CorrelationId,
                        User = context.Saga.User
                    }),
                When(UserCreationFailed)
                    .TransitionTo(Failed)
                    .Finalize()
            );

            During(MessageSendingRequested,
                When(MessageSent)
                    .TransitionTo(Completed)
                    .Finalize(),
                When(MessageSendingFailed)
                    .Publish(context => new DeleteUserCommand
                    {
                        CorrelationId = context.Saga.CorrelationId,
                        Username = context.Saga.User.Username
                    })
                    .TransitionTo(UserDeletionRequested)
            );

            During(UserDeletionRequested,
                When(UserCreationFailed)
                    .TransitionTo(Compensated)
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }

        public State UserCreationRequested { get; private set; }
        public State MessageSendingRequested { get; private set; }
        public State UserDeletionRequested { get; private set; }
        public State Completed { get; private set; }
        public State Compensated { get; private set; }
        public State Failed { get; private set; }

        public Event<IUserCreationInitiated> UserCreationInitiated { get; private set; }
        public Event<IUserCreated> UserCreated { get; private set; }
        public Event<IMessageSent> MessageSent { get; private set; }
        public Event<IUserCreationFailed> UserCreationFailed { get; private set; }
        public Event<IMessageSendingFailed> MessageSendingFailed { get; private set; }
    }

    // Commands and Events
    public interface IUserCreationInitiated
    {
        Guid CorrelationId { get; }
        User User { get; }
    }

    public class UserCreationInitiated() : IUserCreationInitiated
    {
        public Guid CorrelationId { get; set; }
        public required User User { get; set; }

        internal static void Init()
        {
            GlobalTopology.Send.UseCorrelationId<UserCreationInitiated>(x => x.CorrelationId);
        }
    }

    public record CreateUserCommand : ICreateUserCommand
    {
        public Guid CorrelationId { get; set; }
        public required User User { get; set; }
        public object Username { get; internal set; }
    }

    public interface ICreateUserCommand
    {
        Guid CorrelationId { get; set; }
        User User { get; set; }
    }

    public record SendMessageCommand : ISendMessageCommand
    {
        public Guid CorrelationId { get; set; }
        public required User User { get; set; }
    }
    public interface ISendMessageCommand
    {
        Guid CorrelationId { get; set; }
        User User { get; set; }
    }
    public interface IUserCreated
    {
        Guid CorrelationId { get; }
    }

    public record UserCreated : IUserCreated
    {
        public Guid CorrelationId { get; set; }
}
   
    public interface IUserCreationFailed
    {
        Guid CorrelationId { get; }
    }

    public interface IMessageSendingFailed
    {
        Guid CorrelationId { get; }
    }
}
