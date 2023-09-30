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
    }
}
