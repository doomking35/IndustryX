using IndustryX.InfrastructureModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustryX.InfrastructureModels.Models
{
    public record MessageSent : IMessageSent
    {
        public Guid CorrelationId { get; set; }
    }
}
