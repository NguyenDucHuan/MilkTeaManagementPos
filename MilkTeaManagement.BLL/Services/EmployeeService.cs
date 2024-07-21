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
            return _employeeRepository.GetLastEmployee();
        }
    }
}
