using LibraryManagement.Application.Dtos.Book;
using LibraryManagement.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public interface IBookService
    {
        Task<PaginatedList<BookDto>> GetAllBooksAsync(int pageNumber, int pageSize);
        Task<BookDto> GetBookByIdAsync(Guid id);
        Task<BookDto> AddBookAsync(CreateEditBookDto createEditBookDto);
        Task DeleteBookAsync(Guid id);
        Task UpdateBookAsync(Guid id, CreateEditBookDto createEditBookDto);
    }
}
