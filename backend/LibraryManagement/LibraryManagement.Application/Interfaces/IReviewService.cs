using LibraryManagement.Application.Dtos.Review;
using LibraryManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces
{
    public interface IReviewService
    {
        Task<PaginatedList<ReviewDto>> GetAllReviewAsync(int pageNumber, int pageSize);
        Task<ReviewDto> GetReviewByIdAsync(Guid id);
        Task<ReviewDto> AddReviewAsync(ReviewCreateEditDto createEditReviewDto);
        Task DeleteReviewAsync(Guid id);
        Task UpdateReviewAsync(Guid id, ReviewCreateEditDto createEditReviewDto);


        Task<IEnumerable<ReviewDto>> GetReviewsByBookIdAsync(Guid bookId);
        Task<ReviewDto> AddReviewAsync(Guid userId, ReviewCreateEditDto reviewCreateEditDto);
    }
}
