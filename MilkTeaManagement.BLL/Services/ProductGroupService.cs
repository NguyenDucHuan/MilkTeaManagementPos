using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;

namespace MilkTeaManagement.BLL.Services
{
    public class ProductGroupService
    {
        private ProductGroupRepository _repo = new();

        public List<TbGroupProduct> GetAllProductGroup()
        {
            return _repo.GetAll();
        }

        public void AddProductGroup(TbGroupProduct productGroup)
        {
            _repo.Add(productGroup);
        }
    }
}
