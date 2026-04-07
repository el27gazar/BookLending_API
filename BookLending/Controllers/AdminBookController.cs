using BookLending.Application.Interfaces;
using BookLending.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLending.webApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminBookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminBookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("AddBook")]
        public IActionResult AddBook([FromBody] Book book)
        {
            _unitOfWork.Books.Add(book);
            return Ok("Book added successfully.");
        }
        [HttpPut("UpdateBook/{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] Guid id, [FromBody] Book updatedBook)
        {
            var existingBook = await _unitOfWork.Books.GetById(id);
            if (existingBook == null)
            {
                return NotFound("Book not found.");
            }
            await _unitOfWork.Books.Update(updatedBook);
            return Ok("Book updated successfully.");
        }
        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
        {
            var existingBook = await _unitOfWork.Books.GetById(id);
            if (existingBook == null)
            {
                return NotFound("Book not found.");
            }
            await _unitOfWork.Books.Delete(id);
            return Ok("Book deleted successfully.");
        }


    }
}
