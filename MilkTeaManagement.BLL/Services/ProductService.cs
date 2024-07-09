using MilkTeaManagement.DAL.Entities;
using MilkTeaManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaManagement.BLL.Services
{
    public class ProductService
    {
        private IGenericRepository<TbProduct> _genericRepository = null;
        public ProductService()
        {
            this._genericRepository = new GenericRepository<TbProduct>();
        }
        public List<TbProduct> GetAllProductList()
        {
            return (List<TbProduct>)_genericRepository.GetAll();
        }
    }
}
