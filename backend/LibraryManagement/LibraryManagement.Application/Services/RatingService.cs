using AutoMapper;
using LibraryManagement.Application.Dtos.Rating;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Interfaces;
using LibraryManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RatingService(IRatingRepository ratingRepository, IUserRepository userRepository, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<RatingDto> AddRatingAsync(RatingCreateEditDto createEditRatingDto)
        {
            var cateogry = _mapper.Map<Rating>(createEditRatingDto);
            await _ratingRepository.Add(cateogry);
            return _mapper.Map<RatingDto>(cateogry);
        }

        public async Task DeleteRatingAsync(Guid id)
        {
            var rating = await _ratingRepository.GetById(id);
            if (rating != null)
            {
                throw new KeyNotFoundException("Rating not found");
            }
            await _ratingRepository.Delete(id);
        }

        public async Task<PaginatedList<RatingDto>> GetAllRatingAsync(int pageNumber, int pageSize)
        {
            var ratings = await _ratingRepository.GetAll(pageNumber, pageSize);
            return _mapper.Map<PaginatedList<RatingDto>>(ratings);
        }

        public async Task<RatingDto> GetRatingByIdAsync(Guid id)
        {
            var rating = await _ratingRepository.GetById(id);
            return _mapper.Map<RatingDto>(rating);
        }

        public async Task UpdateRatingAsync(Guid id, RatingCreateEditDto createEditRatingDto)
        {
            var rating = await _ratingRepository.GetById(id);
            if (rating != null)
            {
                throw new KeyNotFoundException("Rating not found");
            }

            _mapper.Map(createEditRatingDto, rating);
            await _ratingRepository.Update(rating);
        }

        public async Task<IEnumerable<RatingDto>> GetRatingsByBookIdAsync(Guid bookId)
        {
            var ratings = await _ratingRepository.GetRatingsByBookIdAsync(bookId);
            return _mapper.Map<IEnumerable<RatingDto>>(ratings);
        }

        public async Task<RatingDto> AddRatingAsync(Guid userId, RatingCreateEditDto ratingCreateEditDto)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            var existingRating = await _ratingRepository.GetRatingByUserAndBookAsync(userId, ratingCreateEditDto.BookId);
            if (existingRating != null)
            {
                throw new ApplicationException("User has already rated this book.");
            }

            var rating = _mapper.Map<Rating>(ratingCreateEditDto);
            rating.UserId = userId;

            await _ratingRepository.Add(rating);

            return _mapper.Map<RatingDto>(rating);
        }





    }
}
