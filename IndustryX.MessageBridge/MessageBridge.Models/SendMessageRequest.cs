using IndustryX.MessageBridge.Common.Enums;

namespace IndustryX.MessageBridge.MessageBridge.Models
{
    public class SendMessageRequest
    {
        public required MessageType MessageType { get; set; } // "SMS" or "Email"
        public required string To { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string TemplateName { get; set; } = string.Empty;
        public Dictionary<string, string> Placeholders { get; set; } = new Dictionary<string, string>();
    }
}
