using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Domain.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Author { get; set; }

        public bool IsAvailable { get; set; }

        //borrowed by
        public Guid? BorrowedBy { get; set; }

        public DateTime DueDate { get; set; }


    }
}
