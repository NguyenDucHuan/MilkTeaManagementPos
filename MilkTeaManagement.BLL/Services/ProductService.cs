using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;

namespace MilkTeaManagement.BLL.Services
{
    public class ProductService
    {
        private IGenericRepository<TbProduct> _genericRepository = null;
        private ProductRepository _productRepository = new ProductRepository();
        public ProductService()
        {
            this._genericRepository = new GenericRepository<TbProduct>();
        }
        public List<TbProduct> GetAllProductList()
        {
            return (List<TbProduct>)_genericRepository.GetAll();
        }
        public void AddProduct(TbProduct product)
        {
            _productRepository.Add(product);
        }
    }
}
