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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private readonly ILogger<BookServices> _logger;
        public IBaseRepo<Book> Books { get; private set; }

        public IBookServices BookServices { get; private set; }

        public UnitOfWork(AppDbContext context,ILogger<BookServices> logger)
        {
            _logger = logger;
            _context = context;
            Books = new BaseRepo<Book>(_context);
            BookServices = new BookServices(_context,this,_logger);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> complete()
        {
          return  _context.SaveChanges();
        }
    }
}
