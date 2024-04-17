namespace IndustryX.ServiceUser.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PasswordConfirmed { get; set; }
        public Guid UserGUID { get; private set; }
        public User(int Id, string UserName, string Surname, string Name, string Email, string Password, bool EmailConfirmed, bool PasswordConfirmed)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.Surname = Surname;
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            this.EmailConfirmed = EmailConfirmed;
            this.PasswordConfirmed = PasswordConfirmed;
            this.UserGUID = Guid.NewGuid();
        }
    }
}
