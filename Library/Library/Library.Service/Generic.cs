using Library.DAL;
using Library.DAL.Common;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class Generic : IGeneric
    {
        protected readonly ILibraryContext _context;

        public Generic(ILibraryContext context)
        {
            _context = context;
        }

        //Add
        public async Task<int> Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }
        //Get
        public async Task<T> Get<T>(Guid id) where T : class
        {
            var obj = await _context.Set<T>().FindAsync(id);
            return obj;
        }
        //Delete
        public async Task<int> Delete<T>(Guid id) where T : class
        {
            T entity = await Get<T>(id);
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }
        //Update
        public async Task<int> Update<T>(T entity) where T : class
        {
            _context.Set<T>().AddOrUpdate(entity);
            return await _context.SaveChangesAsync();
        }
        //GetAll
        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        public IQueryable<T> GetQueryable<T>() where T : class
        {
            return _context.Set<T>();
        }


    }
}
