namespace NetCoreApi.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = null;
    }
}
