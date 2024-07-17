using MilkTeaManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                IdCustomer = x.IdCustomer,
                Status = x.Status,
                IdTable = x.IdTable
            };
            _context.TbBills.Add(tbBill);
            _context.SaveChanges();
        }
        public TbBill GetById(long id)
        {
            _context = new MilkTeaContext();
            return _context.TbBills.Where(x => x.Id == id).FirstOrDefault();
        }
        public List<TbBill> GetAll()
        {
            _context = new MilkTeaContext();
            return _context.TbBills.ToList();
        }
        public void Update(TbBill x)
        {
            _context = new MilkTeaContext();
            _context.TbBills.Update(x);
            _context.SaveChanges();
        }
    }
}
