using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaManagement.BLL.Services
{
    public class EmployeeService
    {
        private IGenericRepository<Employee> _genericRepository = null;

        public EmployeeService()
        {
            _genericRepository = new GenericRepository<Employee>();
        }
        public Employee GetLoginEmployee(long id)
        {
            return _genericRepository.GetById(id);
        }
    }
}
