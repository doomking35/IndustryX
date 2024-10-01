using IndustryX.ServiceUser.DAL;
using IndustryX.ServiceUser.Models;
using MassTransit;
using Microsoft.Extensions.Options;

namespace IndustryX.ServiceUser.Sagas
{
    public class UserCreationSagaState : SagaStateMachineInstance,ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public required User User { get; set; }
        public int Version { get; set; }
    }
}
