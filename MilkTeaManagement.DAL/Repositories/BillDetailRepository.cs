using MilkTeaManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaManagement.DAL.Repositories
{
    public class BillDetailRepository
    {
        private MilkTeaContext _context;
        public void Add(TbBillDetailt tbBillDetailt)
        {
            _context = new();
            var newBillDetail = new TbBillDetailt()
            {
                UnitPrice = tbBillDetailt.UnitPrice,
                Quantity = tbBillDetailt.Quantity,
                IdBill = tbBillDetailt.IdBill,
                IdProduct = tbBillDetailt.IdProduct,
                IntoMoney = tbBillDetailt.IntoMoney,
                Description = tbBillDetailt.Description
            };
            _context.TbBillDetailts.Add(newBillDetail);
            _context.SaveChanges();
        }
        public List<TbBillDetailt> GetAll()
        {
            _context = new MilkTeaContext();
            return _context.TbBillDetailts.ToList();
        }
    }
}
