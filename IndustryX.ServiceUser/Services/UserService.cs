using IndustryX.InfrastructureModels.Interfaces;
using IndustryX.InfrastructureModels.Models;
using IndustryX.ServiceUser.DAL;
using IndustryX.ServiceUser.Models;
using IndustryX.ServiceUser.Repositories;
using IndustryX.ServiceUser.Repositories.Interfaces;
using IndustryX.ServiceUser.Sagas;
using IndustryX.ServiceUser.Services.Interfaces;
using MassTransit;
using MassTransit.Transports;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System.Net.Mail;

namespace IndustryX.ServiceUser.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public UserService(IUserRepository userRepository, IPublishEndpoint publishEndpoint)
        {
            _userRepository = userRepository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }
        public async Task<User> GetUserByIdAsync(ObjectId id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
        public async Task CreateUserAsync(User user)
        {
            await _publishEndpoint.Publish<UserCreationInitiated>(new { CorrelationId = Guid.NewGuid(), User = user});

            //await _userRepository.CreateUserAsync(user);
            //await SendMail<MessageBridgeSendMessageRequest>(user);
        }
        public async Task UpdateUserAsync(ObjectId id, User user)
        {
            await _userRepository.UpdateUserAsync(id, user);
        }
        public async Task DeleteUserAsync(ObjectId id)
        {
            await _userRepository.DeleteUserAsync(id);
        }
        public async Task SendMail<T>(object mailInfo)
        {
            if (typeof(T) == typeof(MessageBridgeSendMessageRequest))
            {
                //await _bus.Publish<ISendMessageCommand>(new SendMessageCommand
                //{
                //    CorrelationId = Guid.NewGuid(),
                //    MessageRequest = new MessageBridgeSendMessageRequest()
                //    { MessageType = InfrastructureModels.Enums.MessageType.Email, Subject = "Test", To = "suatalkn@gmail.com" }
                //}
                //);
            }
            else
                throw new Exception("Not handled");
        }
    }
}
