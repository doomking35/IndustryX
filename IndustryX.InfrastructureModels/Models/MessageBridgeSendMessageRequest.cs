using IndustryX.InfrastructureModels.Enums;

namespace IndustryX.InfrastructureModels.Models
{
    public record MessageBridgeSendMessageRequest
    {
        public required MessageType MessageType { get; init; } // "SMS" or "Email"
        public required string To { get; init; }
        public required string Subject { get; init; } = string.Empty;
        public string TemplateName { get; init; } = "default";
        public Dictionary<string, string> Placeholders { get; init; } = new Dictionary<string, string>();
        public required Guid CorrelationId { get; set; }
    }
}
