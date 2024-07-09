using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaManagement.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private MilkTeaContext _context = null;
        private DbSet<T> _table = null;
        public GenericRepository()
        {
            this._context = new MilkTeaContext();
            _table = _context.Set<T>();
        }
        public GenericRepository(MilkTeaContext _context)
        {
            this._context = _context;
            _table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public T GetById(object id)
        {
            return _table.Find(id);
        }

        public void Insert(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            _table.Add(obj);
        }

        public void Update(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            var existing = _table.Find(id);
            if (existing != null)
            {
                _table.Remove(existing);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
