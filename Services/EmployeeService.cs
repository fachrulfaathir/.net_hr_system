namespace NetCoreApi.Services

{

    using Microsoft.EntityFrameworkCore;
    using NetCoreApi.Data;
    using NetCoreApi.DTOs.Employee;
    using NetCoreApi.Entities;

    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<EmployeeResponse> GetAll()
        {
            return _context.Employees
                .Include(e => e.User)
                .Select(e => new EmployeeResponse
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Position = e.Position,
                    Email = e.User.Email
                })
                .ToList();
        }

        public EmployeeResponse GetById(int id)
        {
            var employee = _context.Employees
                .Include(e => e.User)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                throw new Exception("Employee not found");

            return new EmployeeResponse
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Position = employee.Position,
                Email = employee.User.Email
            };
        }

        public EmployeeResponse GetByUserId(int userId)
        {
            var employee = _context.Employees
                .Include(e => e.User)
                .FirstOrDefault(e => e.UserId == userId);

            if (employee == null)
                throw new Exception("Employee not found");

            return new EmployeeResponse
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Position = employee.Position,
                Email = employee.User.Email
            };
        }

        public void Create(CreateEmployeeRequest request)
        {
            var employee = new Employee
            {
                FullName = request.FullName,
                Position = request.Position,
                UserId = request.UserId
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateEmployeeRequest request)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
                throw new Exception("Employee not found");

            employee.FullName = request.FullName;
            employee.Position = request.Position;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
                throw new Exception("Employee not found");

            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }

}
