using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Domain.Models
{
    public class BookDto
    {
        
            public Guid Id { get; set; }
            public string Name { get; set; }
            public bool IsAvailable { get; set; }
            public DateTime? DueDate { get; set; }
        
    }
}
