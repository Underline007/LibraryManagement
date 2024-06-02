using LibraryManagement.Application.Dtos.Rating;
using LibraryManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRatings([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var ratings = await _ratingService.GetAllRatingAsync(pageNumber, pageSize);
            return Ok(ratings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRatingById(Guid id)
        {
            var rating = await _ratingService.GetRatingByIdAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            return Ok(rating);
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetRatingsByBookId(Guid bookId)
        {
            var ratings = await _ratingService.GetRatingsByBookIdAsync(bookId);
            return Ok(ratings);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRating([FromBody] RatingCreateEditDto ratingCreateEditDto)
        {
            var userId = Guid.Parse(User.Identity.Name); // Assuming the user's ID is stored in the identity name
            var rating = await _ratingService.AddRatingAsync(userId, ratingCreateEditDto);
            return CreatedAtAction(nameof(GetRatingById), new { id = rating.Id }, rating);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRating(Guid id, [FromBody] RatingCreateEditDto ratingCreateEditDto)
        {
            await _ratingService.UpdateRatingAsync(id, ratingCreateEditDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRating(Guid id)
        {
            await _ratingService.DeleteRatingAsync(id);
            return NoContent();
        }
    }
}
