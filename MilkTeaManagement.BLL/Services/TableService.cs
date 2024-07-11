using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;

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

        public List<TbTable> GetTableByGroup(long idGroup)
        {
            _repo = new TableRepository();
            return _repo.GetTableByGroup(idGroup);
        }


    }
}
