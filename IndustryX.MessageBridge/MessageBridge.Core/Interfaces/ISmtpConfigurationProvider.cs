using IndustryX.MessageBridge.MessageBridge.Models;

namespace IndustryX.MessageBridge.MessageBridge.Core.Interfaces
{
    public interface ISmtpConfigurationProvider
    {
        SmtpSettings GetSmtpSettings();
    }
}
