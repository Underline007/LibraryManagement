using LibraryManagement.Application.Dtos.BookBorrowingRequest;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces
{
    public interface IBookBorrowingRequestService
    {
        Task<BookBorrowingRequestDto> CreateRequestAsync(BorrowingRequestCreateEditDto createEditDto);
        Task UpdateRequestStatusAsync(Guid requestId, BorrowingRequestStatus newStatus, Guid approverId);
        //Task<IEnumerable<BookBorrowingRequestDto>> GetAllRequestsAsync(int pageNumber, int pageSize);
        Task<BookBorrowingRequestDto> GetRequestByIdAsync(Guid requestId);
        Task NotifyUserAsync(Guid requestId);
        Task<IEnumerable<BookBorrowingRequestDto>> GetRequestsByUserIdAsync(Guid userId);
        Task<IEnumerable<BookBorrowingRequestDto>> GetAllRequestsAsync();

    }
}
