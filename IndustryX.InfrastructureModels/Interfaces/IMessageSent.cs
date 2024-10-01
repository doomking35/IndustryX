using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustryX.InfrastructureModels.Interfaces
{
    public interface IMessageSent
    {
        Guid CorrelationId { get; }
    }
}
