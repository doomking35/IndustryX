using IndustryX.ServiceUser.Models.Enums;
using MongoDB.Bson;

namespace IndustryX.ServiceUser.Models
{
    public class UserConfirmation 
    {
        public ObjectId Id { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public bool PasswordConfirmed { get; private set; }
        public UserStatus UserStatus { get; private set; }
        public Guid UserGUID { get; private set; } 

        public UserConfirmation(bool emailConfirmed, bool passwordConfirmed, UserStatus userStatus, Guid userGUID, ObjectId id)
        {
            Id = id;
            EmailConfirmed = emailConfirmed;
            PasswordConfirmed = passwordConfirmed;
            UserStatus = userStatus;
            UserGUID = userGUID;
        }
        public UserConfirmation() { }
    }
}
