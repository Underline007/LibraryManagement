using LibraryManagement.Application.Dtos.Book;
using LibraryManagement.Application.Services;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBooks(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync(pageNumber, pageSize);
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving books.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving books.");
            }
        }

        [Authorize]
        [HttpGet("query")]
        public async Task<IActionResult> SearchAndSort([FromQuery] string? searchTerm, [FromQuery] string? sortBy)
        {
            var books = await _bookService.SearchAndSortAsync(searchTerm?.ToLower(), sortBy?.ToLower());
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the book.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the book.");
            }
        }

        [Authorize(Roles = nameof(UserRole.SuperUser))]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromForm] CreateEditBookDto createEditBookDto)
        {
            try
            {
                if (createEditBookDto == null)
                {
                    return BadRequest();
                }

                var book = await _bookService.AddBookAsync(createEditBookDto);
                return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the book.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while adding the book.");
            }
        }

        [Authorize(Roles = nameof(UserRole.SuperUser))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Book not found with id: {Id}", id);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the book.");
            }
        }

        [Authorize(Roles = nameof(UserRole.SuperUser))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromForm] CreateEditBookDto createEditBookDto)
        {
            try
            {
                await _bookService.UpdateBookAsync(id, createEditBookDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Book not found with id: {Id}", id);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the book.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while updating the book.");
            }
        }
    }
}
