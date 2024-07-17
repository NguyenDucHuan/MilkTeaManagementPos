using MilkTeaManagement.DAL.Entities;

namespace MilkTeaManagement.DAL.Repositories
{
    public class ProductGroupRepository
    {
        private MilkTeaContext _context;

        public List<TbGroupProduct> GetAll()
        {
            _context = new MilkTeaContext();
            return _context.TbGroupProducts.ToList();
        }

        public void Add(TbGroupProduct productGroup)
        {
            _context = new MilkTeaContext();
            _context.TbGroupProducts.Add(productGroup);
            _context.SaveChanges();
        }
    }
}
