namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Address { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
