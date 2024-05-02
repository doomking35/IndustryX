using IndustryX.ServiceUser.Models;
using MongoDB.Bson;

namespace IndustryX.ServiceUser.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetById(ObjectId id);
        User GetByUserName(string userName);
        void Add(User user);
        void Update(User user);
        void Delete(ObjectId id);
        UserConfirmation GetStatusByUserName(string userName);
    }
}
