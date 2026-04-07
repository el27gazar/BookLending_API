using BookLending.Application.Interfaces;
using BookLending.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Infrastructure.Implementation
{
    public class BookServices : IBookServices
    {
        private readonly AppDbContext dbContext;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<BookServices> logger;

        public BookServices(AppDbContext dbContext, IUnitOfWork unitOfWork, ILogger<BookServices> logger)
        {
            this.dbContext = dbContext;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public async Task BorrowBook(Guid bookid, Guid UserID)
        {
            if (bookid == Guid.Empty)
            {
                throw new ArgumentException("Book ID cannot be empty.");
            }
            if (UserID == Guid.Empty) {
                                throw new ArgumentException("User ID cannot be empty.");
            }
            var book = await unitOfWork.Books.GetById(bookid);
            var alreadyHasBook = await unitOfWork.Books.Find(b => b.BorrowedBy == UserID);
                if(alreadyHasBook.Any())
                {
                    throw new InvalidOperationException("User already has a borrowed book. Please return it before borrowing another.");
                }

            // Check if the book is available (option)
            if (book.IsAvailable)
            {
                book.IsAvailable = false;
                book.BorrowedBy = UserID;
                book.DueDate = DateTime.UtcNow.AddDays(7);
                await unitOfWork.Books.Update(book);
                await unitOfWork.complete();
            }
            else
            {
                throw new InvalidOperationException("This book is currently not available.");
            }
        }

        public async Task<List<Book>> GetAllBooksAvailable()
        {
            return await unitOfWork.Books.Find(b=>b.IsAvailable);

        }

        public async Task ReturnBook(Guid bookid, Guid UserID)
        {
            if (bookid == Guid.Empty)
            {
                throw new ArgumentException("Book ID cannot be empty.");
            }
            if (UserID == Guid.Empty)
            {
                throw new ArgumentException("User ID cannot be empty.");
            }
            var book =await unitOfWork.Books.GetById(bookid);

            if (book.BorrowedBy == UserID)
            {
                book.IsAvailable = true;
                book.BorrowedBy = null;
                await unitOfWork.Books.Update(book);
                await unitOfWork.complete();
            }
            else
            {
                throw new InvalidOperationException("This user did not borrow this book.");
            }
        }

        public async Task CheckOverdueBooks()
        {
            var overdueBooks = await unitOfWork.Books.Find(b =>
                !b.IsAvailable &&
                b.DueDate < DateTime.UtcNow);

            if(!overdueBooks.Any())
            {
                logger.LogInformation("No overdue books found today.");
                return;
            }
            foreach (var book in overdueBooks)
            {
                logger.LogWarning("ALERT: Book '{Name}' (ID: {Id}) is OVERDUE. Borrowed by User: {UserId}. Due Date was: {DueDate}",
                            book.Name, book.Id, book.BorrowedBy, book.DueDate);
            }
        }
    }
}
