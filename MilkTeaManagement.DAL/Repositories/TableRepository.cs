using Microsoft.EntityFrameworkCore;
using MilkTeaManagement.DAL.Entities;

namespace MilkTeaManagement.DAL.Repositories
{
    public class TableRepository
    {
        private MilkTeaContext _context;

        public List<TbTable> GetAll()
        {
            _context = new MilkTeaContext();
            return _context.TbTables.Include(table => table.IdGroupNavigation).ToList();
        }

        public List<TbTable> GetTableByGroup(long idGroup)
        {
            _context = new MilkTeaContext();
            return _context.TbTables.Include(table => table.IdGroupNavigation).Where(table => table.IdGroup == idGroup).ToList();
        }
    }
}
