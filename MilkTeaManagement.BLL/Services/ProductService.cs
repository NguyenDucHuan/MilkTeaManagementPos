using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;

namespace MilkTeaManagement.BLL.Services
{
    public class ProductService
    {
        private ProductRepository _productRepository = new ProductRepository();
        public List<TbProduct> GetAllProductList()
        {
            return _productRepository.GetAll();
        }
        public void AddProduct(TbProduct product)
        {
            _productRepository.Add(product);
        }

        public void UpdateProduct(TbProduct product)
        {
            _productRepository.Update(product);
        }
    }
}
