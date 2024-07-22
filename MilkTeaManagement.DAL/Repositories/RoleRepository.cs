using MilkTeaManagement.DAL.Entities;

namespace MilkTeaManagement.DAL.Repositories
{
    public class RoleRepository
    {
        private MilkTeaContext _context;

        public List<TbRole> GetAll()
        {
            _context = new MilkTeaContext();
            return _context.TbRoles.ToList();
        }

    }
}
