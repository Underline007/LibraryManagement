using AutoMapper;
using LibraryManagement.Application.Dtos.Review;
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
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IUserRepository userRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ReviewDto> AddReviewAsync(ReviewCreateEditDto createEditReviewDto)
        {
            var cateogry = _mapper.Map<Review>(createEditReviewDto);
            await _reviewRepository.Add(cateogry);
            return _mapper.Map<ReviewDto>(cateogry);
        }

        public async Task DeleteReviewAsync(Guid id)
        {
            var review = await _reviewRepository.GetById(id);
            if (review != null)
            {
                throw new KeyNotFoundException("Review not found");
            }
            await _reviewRepository.Delete(id);
        }

        public async Task<PaginatedList<ReviewDto>> GetAllReviewAsync(int pageNumber, int pageSize)
        {
            var reviews = await _reviewRepository.GetAll(pageNumber, pageSize);
            return _mapper.Map<PaginatedList<ReviewDto>>(reviews);
        }

        public async Task<ReviewDto> GetReviewByIdAsync(Guid id)
        {
            var review = await _reviewRepository.GetById(id);
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task UpdateReviewAsync(Guid id, ReviewCreateEditDto createEditReviewDto)
        {
            var review = await _reviewRepository.GetById(id);
            if (review != null)
            {
                throw new KeyNotFoundException("Review not found");
            }

            _mapper.Map(createEditReviewDto, review);
            await _reviewRepository.Update(review);
        }

        public async Task<ReviewDto> AddReviewAsync(Guid userId, ReviewCreateEditDto reviewCreateEditDto)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            var existingReview = await _reviewRepository.GetReviewByUserAndBookAsync(userId, reviewCreateEditDto.BookId);
            if (existingReview != null)
            {
                throw new ApplicationException("User has already reviewed this book.");
            }

            var review = _mapper.Map<Review>(reviewCreateEditDto);
            review.UserId = userId;

            await _reviewRepository.Add(review);

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByBookIdAsync(Guid bookId)
        {
            var reviews = await _reviewRepository.GetReviewsByBookIdAsync(bookId);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }
    }
}
