using AutoMapper;
using LibraryManagement.Application.Dtos.BookBorrowingRequest;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Application.Common;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using LibraryManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper.Internal;

namespace LibraryManagement.Application.Services
{
    public class BookBorrowingRequestService : IBookBorrowingRequestService
    {
        private readonly IBookBorrowingRequestRepository _requestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public BookBorrowingRequestService(IBookBorrowingRequestRepository requestRepository, IUserRepository userRepository, IEmailService emailService, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _userRepository = userRepository;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<BookBorrowingRequestDto> CreateRequestAsync(BorrowingRequestCreateEditDto createEditDto)
        {
            var currentDate = DateTime.Now;
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var userRequests = await _requestRepository.GetRequestsByUserAndDateAsync(createEditDto.RequestorId, firstDayOfMonth, lastDayOfMonth);
            var userRequestCount = userRequests.Count();

            var bookCount = createEditDto.BookIds?.Count() ?? 0;
            if (createEditDto.BookBorrowingReturnDate < DateTime.Now)
            {
                throw new ApplicationException("Return Date must be greater than current date");
            }
            if (userRequestCount >= 3)
            {
                throw new ApplicationException("User has exceeded the maximum borrowing requests allowed for this month.");
            }
            if (bookCount > 5)
            {
                throw new ApplicationException("Maximum 5 books are allowed to borrow at once.");
            }


            var newRequest = new BookBorrowingRequest
            {
                
                RequestorId = createEditDto.RequestorId,
                BookBorrowingReturnDate = createEditDto.BookBorrowingReturnDate,
                Status = BorrowingRequestStatus.Waitting,
                RequestDate = DateTime.Now,
                BookBorrowingRequestDetails = createEditDto.BookIds?.Select(id => new BookBorrowingRequestDetails { BookId = id }).ToList()
            };

            await _requestRepository.Add(newRequest);
            return _mapper.Map<BookBorrowingRequestDto>(newRequest);
        }




        public async Task<BookBorrowingRequestDto> GetRequestByIdAsync(Guid requestId)
        {
            var request = await _requestRepository.GetById(requestId);
            return _mapper.Map<BookBorrowingRequestDto>(request);
        }

       

        public async Task UpdateRequestStatusAsync(Guid requestId, BorrowingRequestStatus newStatus, Guid approverId)
        {
            await _requestRepository.UpdateStatusAsync(requestId, newStatus, approverId);
            await NotifyUserAsync(requestId);
        }



        public async Task NotifyUserAsync(Guid requestId)
        {
            var request = await _requestRepository.GetById(requestId);
            if (request == null)
            {
                throw new KeyNotFoundException("Borrowing request not found.");
            }

            var requestorId = request.RequestorId ?? throw new InvalidOperationException("RequestorId is null.");
            var user = await _userRepository.GetById(requestorId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            var sendMailRequest = new SendMailRequest
            {
                ToEmail = user.Email,
                Subject = "Update on Your Book Borrowing Request",
                Body = $"Your borrowing request with ID {request.Id} has been updated to {request.Status}."
            };

            await _emailService.SendEmailAsync(sendMailRequest);
        }

        public async Task<IEnumerable<BookBorrowingRequestDto>> GetRequestsByUserIdAsync(Guid userId)
        {
            var requests = await _requestRepository.GetRequestsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<BookBorrowingRequestDto>>(requests);
        }

        public async Task<IEnumerable<BookBorrowingRequestDto>> GetAllRequestsAsync()
        {
            var requests = await _requestRepository.GetAllRequestsAsync();
            return _mapper.Map<IEnumerable<BookBorrowingRequestDto>>(requests);
        }
    }
}
