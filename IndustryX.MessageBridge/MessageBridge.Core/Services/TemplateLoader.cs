using IndustryX.MessageBridge.Common.Enums;
using IndustryX.MessageBridge.MessageBridge.Core.Interfaces;
using IndustryX.MessageBridge.MessageBridge.Models;
using Microsoft.Extensions.Options;

namespace IndustryX.MessageBridge.MessageBridge.Core.Services
{
    public class TemplateLoader : ITemplateLoader
    {

        private readonly string _templateDirectory;
        private readonly ILogger<TemplateLoader> _logger; // Loglama için ILogger eklendi

        public TemplateLoader(IOptions<TemplateSettings> options, ILogger<TemplateLoader> logger)
        {
            _templateDirectory = options.Value.TemplateDirectory;
            _logger = logger;
        }

        public async Task<string> LoadTemplateAsync(string templateName, Dictionary<string, string> placeholders)
        {
            var templatePath = Path.Combine(_templateDirectory, $"{templateName}.html");

            if (!File.Exists(templatePath))
            {
                _logger.LogError(null, $"Template file not found: {templatePath}");
                throw new FileNotFoundException($"Template file not found: {templatePath}");
            }

            var template = await File.ReadAllTextAsync(templatePath);

            // Placeholder'ları şablona yerleştir
            foreach (var placeholder in placeholders)
            {
                template = template.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
            }
            _logger.LogInformation("Template named {templateName} loaded successfully.", templateName);
            return template;
        }
    }
}
