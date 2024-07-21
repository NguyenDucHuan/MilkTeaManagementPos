using MilkTeaManagement.DAL.Entities;

namespace MilkTeaManagement.DAL.Repositories
{
    public class EmployeeRepository
    {
        private MilkTeaContext _context;

        public List<Employee> GetAll()
        {
            _context = new MilkTeaContext();
            return _context.Employees.ToList();
        }
        public void Add(Employee employee)
        {
            _context = new MilkTeaContext();
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void Update(Employee employee)
        {
            _context = new MilkTeaContext();
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }
        public void Delete(Employee employee)
        {
            _context = new MilkTeaContext();
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }

        public Employee GetLastEmployee()
        {
            _context = new MilkTeaContext();
            return _context.Employees.OrderByDescending(x => x.Id).FirstOrDefault();
        }

    }
}