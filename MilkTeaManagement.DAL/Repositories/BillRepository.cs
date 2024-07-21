using Microsoft.EntityFrameworkCore;
using MilkTeaManagement.DAL.Entities;

namespace MilkTeaManagement.DAL.Repositories
{
    public class BillRepository
    {
        private MilkTeaContext _context;

        public void AddNew(TbBill x)
        {
            _context = new MilkTeaContext();
            TbBill tbBill = new TbBill()
            {
                TotalMoney = x.TotalMoney,
                Description = x.Description,
                BillDate = x.BillDate,
                IdUser = x.IdUser,
                Status = x.Status,
                IdTable = x.IdTable
            };
            _context.TbBills.Add(tbBill);
            _context.SaveChanges();
        }
        public TbBill GetById(long id)
        {
            _context = new MilkTeaContext();
            return _context.TbBills
                            .Include(b => b.TbBillDetailts)
                            .Where(x => x.Id == id)
                            .FirstOrDefault();
        }
        public List<TbBill> GetAll()
        {
            _context = new MilkTeaContext();
            return _context.TbBills
                .Include(b => b.TbBillDetailts)
                    .ThenInclude(d => d.IdProductNavigation)
                .Include(b => b.IdTableNavigation)
                .Include(b => b.IdUserNavigation)
                .ToList();
        }

        public List<TbBill> GetByDate(DateTime dateTime)
        {
            _context = new MilkTeaContext();
            return _context.TbBills
                .Include(b => b.TbBillDetailts)
                    .ThenInclude(d => d.IdProductNavigation)
                .Include(b => b.IdTableNavigation)
                .Include(b => b.IdUserNavigation)
                .Where(b => b.BillDate.HasValue &&
                            b.BillDate.Value.Date == dateTime.Date)
                .ToList();
        }
        public void Update(TbBill x)
        {
            _context = new MilkTeaContext();
            _context.TbBills.Update(x);
            _context.SaveChanges();
        }

    }
}
