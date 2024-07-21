using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;

namespace MilkTeaManagement.BLL.Services
{
    public class RoleService
    {
        private RoleRepository _roleRepository = new RoleRepository();

        public List<TbRole> GetAllRole()
        {
            return _roleRepository.GetAll();
        }


    }
}
