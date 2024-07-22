using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace MilkTeaManagement.BLL.Services
{
    public class LoginServices
    {
        private readonly LoginRepository _repository;

        public LoginServices()
        {
            _repository = new LoginRepository();
        }

        public bool CheckLoginUser(string userName, string password)
        {
            var loginUser = _repository.GetLogin(userName, HashString(password));
            return loginUser != null;
        }
        public Login GetLogin(string userName, string password)
        {
            return _repository.GetLogin(userName, HashString(password));
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

        public Login GetLoginByEmpID(long loggedInEmpID)
        {
            return _repository.GetLoginByEmpID(loggedInEmpID);
        }
        public Login GetLastEmployee()
        {
            var login = _repository.GetAll();
            return login.OrderByDescending(p => p.Id).FirstOrDefault();
        }
        public List<Login> GetAllAccount()
        {
            return _repository.GetAll();
        }
        public void AddAccount(Login login)
        {
            login.Password = HashString(login.Password);
            _repository.Add(login);
        }

        public void UpdateAccount(Login login)
        {
            _repository.Update(login);
        }

        public void DeleteAccount(Login login)
        {
            _repository.Delete(login);
        }
        public Login GetLoginNoHash(string userName, string password)
        {
            return _repository.GetLogin(userName, password);
        }
    }

}
