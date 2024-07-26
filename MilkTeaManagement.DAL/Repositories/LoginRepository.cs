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
            return _context.Logins.Include(l => l.LoginRoles).Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
        }

        public Login GetLoginByEmpID(long loggedInEmpID)
        {
            _context = new MilkTeaContext();
            return _context.Logins.Include(l => l.LoginRoles).Where(x => x.IdEmployee == loggedInEmpID).SingleOrDefault();
        }

        public List<Login> GetAll()
        {
            _context = new MilkTeaContext();
            return _context.Logins.Include(l => l.LoginRoles).Include(e => e.IdEmployeeNavigation).ToList();
        }
        public void Add(Login login)
        {
            _context = new MilkTeaContext();
            _context.Logins.Add(login);
            _context.SaveChanges();
        }

        public void Update(Login login)
        {
            _context = new MilkTeaContext();
            _context.Logins.Update(login);
            _context.SaveChanges();
        }
        public void Delete(Login login)
        {
            _context = new MilkTeaContext();
            _context.Logins.Remove(_context.Logins.Find(login.Id));
            _context.SaveChanges();
        }
    }
}
