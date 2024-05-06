using IndustryX.ServiceUser.Models.Enums;
using MongoDB.Bson;
using System.ComponentModel;
using System.Text.Json.Serialization;
using ThirdParty.Json.LitJson;

namespace IndustryX.ServiceUser.Models
{
    [Serializable]
    public class User
    {
        [JsonIgnore]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        // [JsonProperty]
        public string UserName { get; set; }
        // [JsonProperty]
        public string Name { get; set; }
        //[JsonProperty]
        public string Surname { get; set; }
        //[JsonProperty]
        public string Email { get; set; }
        //[JsonProperty]
        public string Password { get; set; }
        // [JsonProperty]
        public bool IsEmailConfirmed { get; private set; } = false;
        //  [JsonProperty]
        public bool IsPasswordConfirmed { get; private set; } = false;
        // [JsonProperty]
        public UserStatus UserStatus { get; private set; } = UserStatus.Active;
        public Guid UserGUID { get; private set; } = Guid.NewGuid();

        // Parametre alan bir constructor eklenmiş
        public User(string userName, string surname, string name, string email, string password)
        {
            UserName = userName;
            Surname = surname;
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
