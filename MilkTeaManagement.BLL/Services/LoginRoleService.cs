using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;

namespace MilkTeaManagement.BLL.Services
{

    public class LoginRoleService
    {
        private LoginRoleRepository _loginRoleRepository = new LoginRoleRepository();

        public void AddLoginRole(LoginRole loginRole)
        {
            _loginRoleRepository.Add(loginRole);
        }

        public void DeleteLoginRole(Login login)
        {
            _loginRoleRepository.Delete(login.Id);
        }

    }
}
