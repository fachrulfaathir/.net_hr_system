namespace NetCoreApi.Services
{
    using NetCoreApi.DTOs.Employee;

    public interface IEmployeeService
    {
        IEnumerable<EmployeeResponse> GetAll();
        EmployeeResponse GetById(int id);
        EmployeeResponse GetByUserId(int userId);
        void Create(CreateEmployeeRequest request);
        void Update(int id, UpdateEmployeeRequest request);
        void Delete(int id);
    }

}