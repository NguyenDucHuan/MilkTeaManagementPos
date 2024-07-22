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

        public void DeleteLoginRole(long id)
        {
            _loginRoleRepository.Delete(id);
        }

        public void UpdateLoginRole(LoginRole loginRole)
        {
            _loginRoleRepository.Update(loginRole);
        }
        public LoginRole GetLoginRole(long id)
        {
            return _loginRoleRepository.GetLoginRoleByLoginId(id);
        }
    }
}
