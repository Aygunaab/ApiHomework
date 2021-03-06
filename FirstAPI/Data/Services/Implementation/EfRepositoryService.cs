using Data.DAL;
using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Implementation
{
    public class EfRepositoryService<T> : IRepositoryService<T> where T : class, BaseEntity
    {


        protected readonly AppDbContext _context;

        public EfRepositoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetOne(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task Create(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public Task Create(List<T> items)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            T item =await GetOne(id);
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
        }



        public async Task Update( T item)
        {
            _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> expression, List<string> IncludeProps = null)
        {
            IQueryable<T> db = _context.Set<T>();
            if (IncludeProps != null)
            {
                foreach (var item in IncludeProps)
                {
                    db = db.Include(item);
                }
            }
            db = db.Where(expression);
            return await db.ToListAsync();
        }
    }

}

