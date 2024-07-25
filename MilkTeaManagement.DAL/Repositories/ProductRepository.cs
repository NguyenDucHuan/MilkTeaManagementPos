using Microsoft.EntityFrameworkCore;
using MilkTeaManagement.DAL.Entities;

namespace MilkTeaManagement.DAL.Repositories
{
    public class ProductRepository
    {
        private MilkTeaContext _context;

        public void Add(TbProduct product)
        {
            _context = new MilkTeaContext();
            _context.TbProducts.Add(product);
            _context.SaveChanges();
        }
        public List<TbProduct> GetAll()
        {
            _context = new MilkTeaContext();
            return _context.TbProducts.Include(t => t.IdGroupProductNavigation).ToList();
        }
    }
}
