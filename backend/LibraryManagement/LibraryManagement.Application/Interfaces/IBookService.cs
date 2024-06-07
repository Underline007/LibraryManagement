using LibraryManagement.Application.Dtos.Book;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Repositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public interface IBookService
    {
        Task<PaginatedList<BookDto>> GetAllBooksAsync(int pageNumber, int pageSize);
        //Task<PaginatedList<BookDto>> GetAll(
        //    Expression<Func<Book, bool>> filter = null,
        //    Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy = null,
        //    int pageNumber = 1,
        //    int pageSize = 10);
        Task<IEnumerable<Book>> SearchAndSortAsync(string? searchTerm, string? sortBy);

        Task<BookDto> GetBookByIdAsync(Guid id);
        Task<BookDto> AddBookAsync(CreateEditBookDto createEditBookDto);
        Task DeleteBookAsync(Guid id);
        Task UpdateBookAsync(Guid id, CreateEditBookDto createEditBookDto);
    }
}
