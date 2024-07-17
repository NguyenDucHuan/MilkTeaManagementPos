using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaManagement.BLL.Services
{
    public class BillService
    {
        private BillRepository _repo;

        public void CreateNewBill(TbBill tbBill)
        {
            _repo = new();
            _repo.AddNew(tbBill);
        }

    }
}
