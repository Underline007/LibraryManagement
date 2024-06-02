using LibraryManagement.Application.Common;
using LibraryManagement.Application.Dtos.BookBorrowingRequest;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Application.Services;
using LibraryManagement.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] BorrowingRequestCreateEditDto createEditDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = await _borrowingRequestService.CreateRequestAsync(createEditDto);
            return CreatedAtAction(nameof(GetRequestById), new { id = request.Id }, request);
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

        [HttpGet]
        public async Task<IActionResult> GetAllRequests([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var requests = await _borrowingRequestService.GetAllRequestsAsync(pageNumber, pageSize);
            return Ok(requests);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateRequestStatus(Guid id, [FromBody] BorrowingRequestStatus newStatus)
        {
            await _borrowingRequestService.UpdateRequestStatusAsync(id, newStatus);
            return NoContent();
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail()
        {
            try
            {
                Mailrequest mailRequest = new Mailrequest();
                mailRequest.ToEmail = "keuconnhaquat@gmail.com";
                mailRequest.Subject = "Welcome to NihiraTechiees";
                mailRequest.Body = GetHtmlcontent();
                await _emailService.SendEmailAsync(mailRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string GetHtmlcontent()
        {
            string Response = "<div style=\"width:100%;background-color:lightblue;text-align:center;margin:10px\">";
            Response += "<h1>Welcome to Nihira Techiees</h1>";
            Response += "<img src=\"https://yt3.googleusercontent.com/v5hyLB4am6E0GZ3y-JXVCxT9g8157eSeNggTZKkWRSfq_B12sCCiZmRhZ4JmRop-nMA18D2IPw=s176-c-k-c0x00ffffff-no-rj\" />";
            Response += "<h2>Thanks for subscribed us</h2>";
            Response += "<a href=\"https://www.youtube.com/channel/UCsbmVmB_or8sVLLEq4XhE_A/join\">Please join membership by click the link</a>";
            Response += "<div><h1> Contact us : nihiratechiees@gmail.com</h1></div>";
            Response += "</div>";
            return Response;
        }
    }
}
