namespace IndustryX.MessageBridge.MessageBridge.Core.Interfaces
{
    public interface ITemplateLoader
    {
        Task<string> LoadTemplateAsync(string templateName, Dictionary<string, string> placeholders);
    }
}
