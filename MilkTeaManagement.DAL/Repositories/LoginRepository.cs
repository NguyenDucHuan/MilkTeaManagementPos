using MilkTeaManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaManagement.DAL.Repositories
{
    public class LoginRepository
    {
        private MilkTeaContext _context;

        public Login GetLogin(string username, string password)
        {
            _context = new MilkTeaContext();
            return _context.Logins.Where(x => x.UserName == username && x.Password == password).SingleOrDefault();
        }
    }
}
