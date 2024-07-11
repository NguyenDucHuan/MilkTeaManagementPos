using MilkTeaManagement.DAL.Entities;

namespace MilkTeaManagement.DAL.Repositories
{
    public class TableGroupRepository
    {
        private MilkTeaContext _context;

        public List<TbGroupTb> GetAll()
        {
            _context = new MilkTeaContext();
            return _context.TbGroupTbs.ToList();
        }
    }
}
