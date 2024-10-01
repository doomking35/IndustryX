using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustryX.InfrastructureModels.Interfaces
{
    public interface IDeleteUserCommand
    {
        Guid CorrelationId { get; init; }
        string Username { get; init; }
    }
}
