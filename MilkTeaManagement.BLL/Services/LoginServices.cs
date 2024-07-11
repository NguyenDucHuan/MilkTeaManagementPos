using MilkTeaManagement.DAL.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace MilkTeaManagement.BLL.Services
{
    public class LoginServices
    {
        private readonly LoginRepository _repository;

        public LoginServices(LoginRepository repository)
        {
            _repository = repository;
        }

        public bool CheckLoginUser(string userName, string password)
        {
            var loginUser = _repository.GetLogin(userName, HashString(password));
            return loginUser != null;
        }

        public long GetEmployeeID(string userName, string password)
        {
            var loginUser = _repository.GetLogin(userName, HashString(password));
            return loginUser?.IdEmployee ?? 0;
        }

        private string HashString(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString().Substring(0, 32);
            }
        }
    }

}
