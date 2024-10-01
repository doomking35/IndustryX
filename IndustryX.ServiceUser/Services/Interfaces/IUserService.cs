using IndustryX.ServiceUser.Models;
using MongoDB.Bson;

namespace IndustryX.ServiceUser.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(ObjectId id);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(ObjectId id, User user);
        Task DeleteUserAsync(ObjectId id);
        Task SendMail<T>(object mailInfo);
    }
}
