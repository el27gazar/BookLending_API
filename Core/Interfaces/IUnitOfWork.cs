using BookLending.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepo<Book> Books { get; }
        IBookServices BookServices { get; }


        Task<int> complete();

       
    }
}
