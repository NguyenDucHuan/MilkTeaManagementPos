using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaManagement.BLL.Services
{
    public class LoginServices
    {
        private LoginRepository _repository;
        public LoginServices()
        {
            this._repository = new LoginRepository();
        }
        public bool CheckLoginUser(string userName, string password)
        {
            var loginUser = _repository.GetLogin(userName, password);
            if (loginUser != null)
                return true;
            return false;
        }
        public long GetEmployeeID(string userName, string password)
        {
            return (long)_repository.GetLogin(userName, password).IdEmployee;
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
