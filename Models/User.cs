namespace Models
{
    using Requests;

    public class User: Model
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Staff { get; set; }

        public User() { }

        public User(RegisterModel m)
        {
            Email = m.Email;
            Password = m.Password;
            Name = m.Name;
            Staff = false;
        }
    }
}
