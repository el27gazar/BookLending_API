using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.DTOsModel
{
    public class DTOsLogin
    {
        public string Password { get; set; } = string.Empty;
        public string? Email { get; set; }

    }
}
