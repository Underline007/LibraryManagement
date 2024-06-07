using LibraryManagement.Application.Dtos.Review;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        [Authorize(Roles = nameof(UserRole.SuperUser))]
        public async Task<IActionResult> GetAllReviews([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var reviews = await _reviewService.GetAllReviewAsync(pageNumber, pageSize);
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(Guid id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetReviewsByBookId(Guid bookId)
        {
            var reviews = await _reviewService.GetReviewsByBookIdAsync(bookId);
            return Ok(reviews);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview([FromBody] ReviewCreateEditDto reviewCreateEditDto)
        {
            var userId = Guid.Parse(User.Identity.Name); 
            var review = await _reviewService.AddReviewAsync(userId, reviewCreateEditDto);
            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, review);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateReview(Guid id, [FromBody] ReviewCreateEditDto reviewCreateEditDto)
        {
            await _reviewService.UpdateReviewAsync(id, reviewCreateEditDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            await _reviewService.DeleteReviewAsync(id);
            return NoContent();
        }
    }
}
