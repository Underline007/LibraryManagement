﻿using AutoMapper;
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
            var request = _mapper.Map<BookBorrowingRequest>(createEditDto);
            await _requestRepository.Add(request);
            return _mapper.Map<BookBorrowingRequestDto>(request);
        }

        public async Task<IEnumerable<BookBorrowingRequestDto>> GetAllRequestsAsync(int pageNumber, int pageSize)
        {
            var requests = await _requestRepository.GetAll(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<BookBorrowingRequestDto>>(requests);
        }

        public async Task<BookBorrowingRequestDto> GetRequestByIdAsync(Guid requestId)
        {
            var request = await _requestRepository.GetById(requestId);
            return _mapper.Map<BookBorrowingRequestDto>(request);
        }

        public async Task UpdateRequestStatusAsync(Guid requestId, BorrowingRequestStatus newStatus)
        {
            await _requestRepository.UpdateStatusAsync(requestId, newStatus);
            await NotifyUserAsync(requestId);
        }

        public async Task NotifyUserAsync(Guid requestId)
        {
            var request = await _requestRepository.GetById(requestId);
            if (request == null)
            {
                throw new KeyNotFoundException("Borrowing request not found.");
            }

            var user = await _userRepository.GetById(request.RequestorId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            var mailRequest = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Update on Your Book Borrowing Request",
                Body = $"Your borrowing request with ID {request.Id} has been updated to {request.Status}."
            };

            await _emailService.SendEmailAsync(mailRequest);
        }
    }
}