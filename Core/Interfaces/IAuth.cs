using BookLending.Application.DTOsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.Interfaces
{
    public interface IAuth
    {
        Task<AuthDTO> Register(DTOsRegister dtregister);

        Task<AuthDTO> Login(DTOsLogin dtlogin);


    }
}
