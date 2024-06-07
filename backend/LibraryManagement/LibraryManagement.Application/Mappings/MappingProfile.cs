using AutoMapper;
using LibraryManagement.Application.Dtos.Auth;
using LibraryManagement.Application.Dtos.Book;
using LibraryManagement.Application.Dtos.BookBorrowingRequest;
using LibraryManagement.Application.Dtos.BookBorrowingRequestDetail;
using LibraryManagement.Application.Dtos.Category;
using LibraryManagement.Application.Dtos.Rating;
using LibraryManagement.Application.Dtos.Review;
using LibraryManagement.Application.Dtos.User;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Repositories;

namespace LibraryManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<CreateEditBookDto, Book>();
            CreateMap<User, UserDto>();
            CreateMap<PaginatedList<User>, PaginatedList<UserDto>>();
            CreateMap<RegisterDto, User>();
            CreateMap<Category, CategoryDto>();
            CreateMap<PaginatedList<Category>, PaginatedList<CategoryDto>>();
            CreateMap<CategoryCreateEditDto, Category>();

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewCreateEditDto, Review>();

            CreateMap<Rating, RatingDto>();
            CreateMap<RatingCreateEditDto, Rating>();

            CreateMap<BookBorrowingRequest, BookBorrowingRequestDto>()
                .ForMember(dest => dest.BookBorrowingRequestDetails, opt => opt.MapFrom(src => src.BookBorrowingRequestDetails));

            CreateMap<BorrowingRequestCreateEditDto, BookBorrowingRequest>();

            CreateMap<BookBorrowingRequestDetails, BookBorrowingRequestDetailDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
        }
    }
}
