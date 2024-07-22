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
            _context.LoginRoles.Remove(_context.LoginRoles.Where(i => i.Id == id).FirstOrDefault());
            _context.SaveChanges();
        }

        public LoginRole GetLoginRoleByLoginId(long loginId)
        {
            _context = new();
            return _context.LoginRoles.FirstOrDefault(p => p.IdLogin == loginId);
        }

        public void Update(LoginRole loginRole)
        {
            _context = new();
            var old = _context.LoginRoles.Where(l => l.IdLogin == loginRole.IdLogin).FirstOrDefault();
            old.IdRole = loginRole.IdRole;
            _context.Update(old);
            _context.SaveChanges();
        }
    }
}
