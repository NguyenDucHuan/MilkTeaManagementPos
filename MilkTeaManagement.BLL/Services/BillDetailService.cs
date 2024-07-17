using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaManagement.BLL.Services
{
    public class BillDetailService
    {
        private BillDetailRepository _billDetailRepository;
        private BillRepository _billRepository;

        public void CreateNewBillDetails(List<TbBillDetailt> tbBillDetailts)
        {
            _billDetailRepository = new BillDetailRepository();
            _billRepository = new BillRepository();
            var newestBillID = _billRepository.GetAll().Max(x => x.Id);
            foreach (var i in tbBillDetailts)
            {
                i.IdBill = newestBillID;
                _billDetailRepository.Add(i);
            }
        }
    }
}
