using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;
using MilkTeaManagement.DAL.ViewModelEntities;

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
        public ReportViewModel GetReportViewModelLast30Day()
        {
            var viewModel = new ReportViewModel();
            var listBill = _repo.GetBillsLast30Day();
            int numPro = 0;
            double totalMoney = 0;
            viewModel.TotalBill = listBill.Count();
            foreach (var bill in listBill)
            {
                totalMoney += (double)bill.TotalMoney;
                foreach (var billDetail in bill.TbBillDetailts)
                {
                    numPro += (int)billDetail.Quantity;
                }
            }
            viewModel.SellProductOnMonth = numPro;
            viewModel.TotalAmount = (int)totalMoney;
            return viewModel;
        }
        public List<double> GetLast30DayTotalMoney()
        {
            var list = _repo.GetBillsLast30Day();
            var otm = new List<double>();
            for (int i = 0; i < 4; i++)
            {
                var endDate = DateTime.Now.AddDays(-i * 7);
                var startDate = endDate.AddDays(-7);
                double weeklyTotal = list
                    .Where(d => d.BillDate >= startDate && d.BillDate < endDate)
                    .Sum(s => s.TotalMoney ?? 0);

                otm.Add(weeklyTotal);
            }
            
            otm.Reverse();
            return otm;
        }

    }
}
