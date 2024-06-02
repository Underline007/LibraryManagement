using AutoMapper;
using LibraryManagement.Application.Dtos.Auth;
using LibraryManagement.Application.Dtos.Book;
using LibraryManagement.Application.Dtos.Category;
using LibraryManagement.Application.Dtos.Rating;
using LibraryManagement.Application.Dtos.Review;
using LibraryManagement.Application.Dtos.User;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<CreateEditBookDto, Book>();
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateEditDto, Category>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewCreateEditDto, Review>();
            CreateMap<Rating, RatingDto>();
            CreateMap<RatingCreateEditDto, Rating>();
        }
    }
}
