using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;

namespace MilkTeaManagement.BLL.Services
{
    public class TableGroupService
    {
        private TableGroupRepository _repo;

        public List<TbGroupTb> GetTableGroupList()
        {
            _repo = new TableGroupRepository();
            return _repo.GetAll();
        }

    }
}
