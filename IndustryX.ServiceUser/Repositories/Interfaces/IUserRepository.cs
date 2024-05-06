using IndustryX.ServiceUser.Models;
using MongoDB.Bson;

namespace IndustryX.ServiceUser.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByUserName(string userName);
        User GetStatusByUserName(string userName);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
       
    }
}
