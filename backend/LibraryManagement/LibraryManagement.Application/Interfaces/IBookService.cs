// IBookService.cs
using LibraryManagement.Application.Dtos.Book;
using System;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync(int pageNumber, int pageSize);
        Task<BookDto> GetBookByIdAsync(Guid id);
        Task<BookDto> AddBookAsync(CreateEditBookDto createEditBookDto);
        Task DeleteBookAsync(Guid id);
        Task UpdateBookAsync(Guid id, CreateEditBookDto createEditBookDto);
    }
}
