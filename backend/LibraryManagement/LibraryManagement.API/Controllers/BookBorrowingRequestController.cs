using LibraryManagement.Application.Common;
using LibraryManagement.Application.Dtos.BookBorrowingRequest;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryManagement.API.Controllers
{
    [Route("api/bookborrowingrequests")]
    [ApiController]
    
    public class BookBorrowingRequestController : ControllerBase
    {
        private readonly IBookBorrowingRequestService _borrowingRequestService;
        private readonly IEmailService _emailService;

        public BookBorrowingRequestController(IBookBorrowingRequestService borrowingRequestService, IEmailService emailService)
        {
            _borrowingRequestService = borrowingRequestService;
            _emailService = emailService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] BorrowingRequestCreateEditDto createEditDto)
        {
            try
            {
                var request = await _borrowingRequestService.CreateRequestAsync(createEditDto);
                return CreatedAtAction(nameof(GetRequestById), new { id = request.Id }, request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestById(Guid id)
        {
            var request = await _borrowingRequestService.GetRequestByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }

        [Authorize(Roles = nameof(UserRole.SuperUser))]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateRequestStatus(Guid id, BorrowingRequestStatus newStatus, Guid approverId)
        {
            await _borrowingRequestService.UpdateRequestStatusAsync(id, newStatus, approverId);
            return NoContent();
        }

        [Authorize]
        [HttpGet("userRequests/{userId}")]
        public async Task<IActionResult> GetUserRequests(Guid userId)
        {
            var requests = await _borrowingRequestService.GetRequestsByUserIdAsync(userId);
            return Ok(requests);
        }

        [Authorize(Roles = nameof(UserRole.SuperUser))]
        [HttpGet("manageRequests")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllRequests()
        {
            var requests = await _borrowingRequestService.GetAllRequestsAsync();
            return Ok(requests);
        }
    }
}
