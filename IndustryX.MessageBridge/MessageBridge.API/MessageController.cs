using IndustryX.MessageBridge.MessageBridge.Core;
using IndustryX.MessageBridge.MessageBridge.Core.Services;
using IndustryX.MessageBridge.MessageBridge.Models;
using Microsoft.AspNetCore.Mvc;

namespace IndustryX.MessageBridge.MessageBridge.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("QueueMessage")]
        public IActionResult QueueMessage([FromBody] SendMessageRequest request)
        {
            _messageService.QueueMessage(request.MessageType, request.To, request.Subject, request.TemplateName, request.Placeholders);
            return Ok("Message queued successfully!");
        }
    }
}
