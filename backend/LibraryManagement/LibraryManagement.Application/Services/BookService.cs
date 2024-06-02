using AutoMapper;
using LibraryManagement.Application.Dtos.Book;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public BookService(IGenericRepository<Book> bookRepository, IPhotoService photoService, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _photoService = photoService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(int pageNumber, int pageSize)
        {
            var books = await _bookRepository.GetAll(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<BookDto>>(books);
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
    }
}
