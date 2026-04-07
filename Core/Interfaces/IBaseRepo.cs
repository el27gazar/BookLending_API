using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.Interfaces
{
    public interface IBaseRepo<T> where T : class
    { 
        //CRUDs operations
        Task<List<T>> GetAll();
        Task Update(T entity);
        Task Delete(Guid id);
        Task Add(T entity);

        Task<T> GetById(Guid id);

        Task<List<T>> Find(Expression<Func<T, bool>> predicate);

    }
}
