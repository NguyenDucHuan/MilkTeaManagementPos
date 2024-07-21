using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;

namespace MilkTeaManagement.BLL.Services
{
    public class EmployeeService
    {
        private IGenericRepository<Employee> _genericRepository = null;
        private EmployeeRepository _employeeRepository = new EmployeeRepository();

        public EmployeeService()
        {
            _genericRepository = new GenericRepository<Employee>();
        }
        public Employee GetLoginEmployee(long id)
        {
            return _genericRepository.GetById(id);
        }

        public void AddEmployee(Employee employee)
        {
            _employeeRepository.Add(employee);
        }

        public Employee GetLastEmployee()
        {
            var emp = _employeeRepository.GetAll();
            return emp.OrderByDescending(p => p.Id).FirstOrDefault();
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.Update(employee);
        }
        public void DeleteEmployee(Employee employee)
        {
            _employeeRepository.Delete(employee);
        }
        public Employee GetEmployeeById(long id)
        {
            return _employeeRepository.GetAll().FirstOrDefault(p => p.Id == id);
        }
    }
}
