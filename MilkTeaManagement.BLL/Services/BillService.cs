using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;

namespace MilkTeaManagement.BLL.Services
{
    public class BillService
    {
        private BillRepository _repo = new();

        public void CreateNewBill(TbBill tbBill)
        {
            _repo = new();
            _repo.AddNew(tbBill);
        }
        public TbBill GetLastestBillFromTableID(long tableID)
        {
            var bills = _repo.GetAll();
            return bills.Where(t => t.IdTable == tableID).ToList().OrderByDescending(p => p.BillDate).FirstOrDefault();
        }

        public List<TbBill> GetAllBills()
        {
            return _repo.GetAll();
        }

        public List<TbBill> GetByDate(DateTime dateTime)
        {
            return _repo.GetByDate(dateTime);
        }
    }
}
