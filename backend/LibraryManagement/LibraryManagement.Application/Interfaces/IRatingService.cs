using LibraryManagement.Application.Dtos.Rating;
using LibraryManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces
{
    public interface IRatingService
    {
        Task<PaginatedList<RatingDto>> GetAllRatingAsync(int pageNumber, int pageSize);
        Task<RatingDto> GetRatingByIdAsync(Guid id);
        Task<RatingDto> AddRatingAsync(RatingCreateEditDto createEditRatingDto);
        Task DeleteRatingAsync(Guid id);
        Task UpdateRatingAsync(Guid id, RatingCreateEditDto createEditRatingDto);

        Task<IEnumerable<RatingDto>> GetRatingsByBookIdAsync(Guid bookId);
        Task<RatingDto> AddRatingAsync(Guid userId, RatingCreateEditDto ratingCreateEditDto);
    }
}
