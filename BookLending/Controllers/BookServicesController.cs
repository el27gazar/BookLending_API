using AutoMapper;
using BookLending.Application.Interfaces;
using BookLending.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLending.webApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookServicesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookServicesController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Authorize]
        [HttpPost("BorrowBook/{bookId}")]
        public async Task<IActionResult> BorrowBook([FromRoute] Guid bookId)
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var userId = Guid.Parse(currentUserId);

                await _unitOfWork.BookServices.BorrowBook(bookId, userId);
                return Ok("Book borrowed successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("ReturnBook/{bookId}")]
        public async Task<IActionResult> ReturnBook([FromRoute] Guid bookId)
        {
            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var userId = Guid.Parse(currentUserId);

                await _unitOfWork.BookServices.ReturnBook(bookId, userId);
                return Ok("Book returned successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);


            }
        }

        [Authorize]
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAvailableBooks()
        {
            try
            {
                var books = await _unitOfWork.BookServices.GetAllBooksAvailable();
                var bookDtos = _mapper.Map<List<BookDto>>(books);
                return Ok(bookDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
