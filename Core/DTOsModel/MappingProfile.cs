using AutoMapper;
using BookLending.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.DTOsModel
{
    public class MappingProfile:Profile
    {
          public MappingProfile()
        {
            CreateMap<Book, BookDto>();
        }

    }
}
