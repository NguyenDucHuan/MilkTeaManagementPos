using Microsoft.EntityFrameworkCore;
using MilkTeaManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaManagement.DAL.Repositories
{
    public class TableRepository
    {
        private MilkTeaContext _context;

        public List<TbTable> GetAll()
        {
            _context = new MilkTeaContext();
            return _context.TbTables.Include("TbGroupTb").ToList();
        }
    }
}
