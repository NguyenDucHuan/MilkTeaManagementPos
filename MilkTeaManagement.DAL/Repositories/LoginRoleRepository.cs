using MilkTeaManagement.DAL.Entities;

namespace MilkTeaManagement.DAL.Repositories
{
    public class LoginRoleRepository
    {
        private MilkTeaContext _context;

        public void Add(LoginRole loginRole)
        {
            _context = new();
            _context.LoginRoles.Add(loginRole);
            _context.SaveChanges();
        }
        public void Delete(long id)
        {
            _context = new();
            _context.LoginRoles.Remove(_context.LoginRoles.Where(t => t.IdLogin == id).FirstOrDefault());
            _context.SaveChanges();
        }

        public LoginRole GetLoginRoleByLoginId(long loginId)
        {
            _context = new();
            return _context.LoginRoles.FirstOrDefault(p => p.IdLogin == loginId);
        }
    }
}
