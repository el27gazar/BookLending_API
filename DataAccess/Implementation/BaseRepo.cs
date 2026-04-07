using BookLending.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Infrastructure.Implementation
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly AppDbContext _context;

        public BaseRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
             await _context.Set<T>().AddAsync(entity);
            
        }

        public async Task Delete(Guid id)
        {
           var item =await GetById(id);
            if (item == null)
            {
                throw new Exception($"Entity of type {typeof(T).Name} with id {id} not found.");
            }
            _context.Set<T>().Remove(item);
             
        }

        public Task<List<T>> Find(Expression<Func<T, bool>> predicate)
        {
           return _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            if (item == null)
            {
             throw new Exception($"Entity of type {typeof(T).Name} with id {id} not found.");
            }
            return item;
        }

        public Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }
    }
}
