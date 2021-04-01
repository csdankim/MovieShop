using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly MovieShopDbContext _dbContext;    // protected -> will be inherited
        public EfRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task<T> GetByIdAsync(int id)    // virtual method can be overriden
        {
            //_dbContext.Movies.ToList();
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync()
        {
            //var test = _dbContext.Movies.Where(m => m.Id == 2).FirstOrDefaultAsync();
            //var testt = new List<MovieCardResponseModel>();
            //var x = testt.Where(m => m.Id == 22).ToList();
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().Where(filter).ToListAsync();
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return await _dbContext.Set<T>().Where(filter).CountAsync();
            }

            return await _dbContext.Set<T>().CountAsync();
        }

        public virtual async Task<bool> GetExistAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
            {
                return false;
            }

            return await _dbContext.Set<T>().Where(filter).AnyAsync();

            // list = 1, 2, 3, 4, 5, 2
             // input 2 --> true
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            //Movie - Id, title, revenue, budget, 55
            _dbContext.Set<T>().Add(entity);            // check video
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            // Disconnected way of doing things
            //Movie - Id, title, revenue, budget, 55
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;

        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
