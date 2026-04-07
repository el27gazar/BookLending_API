using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.DTOsModel
{
    public class AuthDTO
    {
        public string message {  get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public bool IsAuthenticated { get; set; }

        public string? Token { get; set; }
        public DateTime TokenExpiration { get; set; }

    }
}
