namespace Domain
{
    public class Session
    {
        public Guid Id { get; set; }
        public User User { get; set; }

        public Session()
        {
            Id = Guid.NewGuid();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Session) return false;
            Session other = obj as Session;
            return Id == other.Id;
        }
    }
}
