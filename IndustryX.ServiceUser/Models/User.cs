using MongoDB.Bson;

namespace IndustryX.ServiceUser.Models
{
    public class User(ObjectId id, string UserName, string Surname, string Name, string Email, string Password)
    {
        public ObjectId Id { get; set; } = id == null ? ObjectId.GenerateNewId() : id;
        public string UserName { get; set; } = UserName;
        public string Name { get; set; } = Name;
        public string Surname { get; set; } = Surname;
        public string Email { get; set; } = Email;
        public string Password { get; set; } = Password;
        public Guid UserGUID { get; private set; } = Guid.NewGuid();
    }
}
