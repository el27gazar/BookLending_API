using BookLending.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.Interfaces
{
    public interface IBookServices
    {
        Task<List<Book>> GetAllBooksAvailable();
        Task BorrowBook(Guid bookid, Guid UserID);
        Task ReturnBook(Guid bookid, Guid UserID);
        Task CheckOverdueBooks();
    }
}
