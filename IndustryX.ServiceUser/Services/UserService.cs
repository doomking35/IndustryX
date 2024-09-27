using IndustryX.InfrastructureModels.Models;
using IndustryX.ServiceUser.DAL;
using IndustryX.ServiceUser.Models;
using IndustryX.ServiceUser.Repositories;
using IndustryX.ServiceUser.Repositories.Interfaces;
using IndustryX.ServiceUser.Services.Interfaces;
using MassTransit;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System.Net.Mail;

namespace IndustryX.ServiceUser.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public UserService(IUserRepository userRepository, IBus bus)
        {
            _userRepository = userRepository;
            _bus = bus;
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }
        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
        public async Task CreateUserAsync(User user)
        {
            await _userRepository.CreateUserAsync(user);
            await SendMail<MessageBridgeSendMessageRequest>(user);
        }
        public async Task UpdateUserAsync(string id, User user)
        {
            await _userRepository.UpdateUserAsync(id, user);
        }
        public async Task DeleteUserAsync(string id)
        {
            await _userRepository.DeleteUserAsync(id);
        }
        public async Task SendMail<T>(object mailInfo)
        {
            if (typeof(T) == typeof(MessageBridgeSendMessageRequest))
            {
                await _bus.Publish<MessageBridgeSendMessageRequest>(new MessageBridgeSendMessageRequest { MessageType = InfrastructureModels.Enums.MessageType.Email, Subject = "Test", To = "suatalkn@gmail.com" });
            }
            else
                throw new Exception("Not handled");
        }
    }
}
