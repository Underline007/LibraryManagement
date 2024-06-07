using AutoMapper;
using LibraryManagement.Application.Dtos.Book;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Interfaces;
using LibraryManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IPhotoService photoService, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _photoService = photoService;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BookDto>> GetAllBooksAsync(int pageNumber, int pageSize)
        {
            var paginatedBooks = await _bookRepository.GetAll(pageNumber, pageSize);
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(paginatedBooks.Items);

            return new PaginatedList<BookDto>
            {
                Items = (List<BookDto>)bookDtos,
                TotalPages = paginatedBooks.TotalPages,

            }; 
        }

        public async Task<BookDto> GetBookByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetById(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> AddBookAsync(CreateEditBookDto createEditBookDto)
        {
            var book = _mapper.Map<Book>(createEditBookDto);

            if (createEditBookDto.Image != null)
            {
                var uploadResult = await _photoService.AddPhotoAsync(createEditBookDto.Image);
                book.Image = uploadResult.Url.ToString();
            }

            await _bookRepository.Add(book);
            return _mapper.Map<BookDto>(book);
        }

        public async Task DeleteBookAsync(Guid id)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found");
            }

            if (!string.IsNullOrEmpty(book.Image))
            {
                await _photoService.DeletePhotoAsync(book.Image);
            }

            await _bookRepository.Delete(id);
        }

        public async Task UpdateBookAsync(Guid id, CreateEditBookDto createEditBookDto)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found");
            }

            _mapper.Map(createEditBookDto, book);

            if (createEditBookDto.Image != null)
            {
                if (!string.IsNullOrEmpty(book.Image))
                {
                    await _photoService.DeletePhotoAsync(book.Image);
                }

                var uploadResult = await _photoService.AddPhotoAsync(createEditBookDto.Image);
                book.Image = uploadResult.Url.ToString();
            }

            await _bookRepository.Update(book);
        }
        public async Task<IEnumerable<Book>> SearchAndSortAsync(string? searchTerm, string? sortBy)
        {
            return await _bookRepository.SearchAndSortAsync(searchTerm, sortBy);
        }

        //public async Task<IEnumerable<Book>> GetBooksByTitleAndAuthor(string title, string author, int pageNumber = 1, int pageSize = 10)
        //{
        //    return await _bookRepository.Search(
        //    b => b.Title.Contains(title) && b.Author.Contains(author),
        //    q => q.OrderBy(b => b.Title),
        //    pageNumber,
        //    pageSize
        //    );
        //}

        //public async Task<PaginatedList<BookDto>> GetAll(Expression<Func<Book, bool>> filter,
        //                                                Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy,
        //                                                int pageNumber,
        //                                                int pageSize)
        //{
        //    var books = await _bookRepository.GetAll(filter, orderBy, pageNumber, pageSize);
        //    return _mapper.Map<PaginatedList<BookDto>>(books);
        //}
    }
}
