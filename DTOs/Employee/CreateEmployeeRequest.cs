namespace NetCoreApi.DTOs.Employee
{
    public class CreateEmployeeRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

}