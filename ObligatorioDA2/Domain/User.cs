namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public IEnumerable<EUserRole> Roles { get; set; }
        public User()
        {
            Id = Guid.NewGuid();
            Roles = new List<EUserRole> { EUserRole.Customer };
        }
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not User) return false;
            User other = obj as User;
            return Id == other.Id;
        }
    }
}
