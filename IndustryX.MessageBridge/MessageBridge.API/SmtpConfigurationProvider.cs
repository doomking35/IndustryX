using IndustryX.MessageBridge.MessageBridge.Core.Interfaces;
using IndustryX.MessageBridge.MessageBridge.Models;

namespace IndustryX.MessageBridge.MessageBridge.API
{
    public class SmtpConfigurationProvider : ISmtpConfigurationProvider
    {
        private readonly IConfiguration _configuration;

        public SmtpConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SmtpSettings GetSmtpSettings()
        {
            return _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
        }
    }
}
