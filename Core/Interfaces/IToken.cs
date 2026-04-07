using BookLending.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.Interfaces
{
    public interface IToken
    {
        Task<string> GenerateAccessToken(Appuser user);
    }
}
