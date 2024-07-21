using Microsoft.EntityFrameworkCore;
using MilkTeaManagement.DAL.Entities;

namespace MilkTeaManagement.DAL.Repositories
{
    public class LoginRepository
    {
        private MilkTeaContext _context;

        public Login GetLogin(string username, string password)
        {
            _context = new MilkTeaContext();
            return _context.Logins.Include(l => l.LoginRoles).Where(x => x.UserName == username && x.Password == password).SingleOrDefault();
        }

        public Login GetLoginByEmpID(long loggedInEmpID)
        {
            _context = new MilkTeaContext();
            return _context.Logins.Include(l => l.LoginRoles).Where(x => x.IdEmployee == loggedInEmpID).SingleOrDefault();
        }
    }
}
