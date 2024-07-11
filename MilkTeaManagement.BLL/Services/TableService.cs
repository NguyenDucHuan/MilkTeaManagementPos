using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaManagement.BLL.Services
{
    public class TableService
    {
        private TableRepository _repo;

        public List<TbTable> GetTableList()
        {
            _repo = new TableRepository();
            return _repo.GetAll();
        }
    }
}
