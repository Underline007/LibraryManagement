using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using System;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Interfaces
{
    public interface IBookBorrowingRequestRepository : IGenericRepository<BookBorrowingRequest>
    {
        Task UpdateStatusAsync(Guid requestId, BorrowingRequestStatus newStatus, Guid approverId);
        Task<IEnumerable<BookBorrowingRequest>> GetRequestsByUserAndDateAsync(Guid userId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<BookBorrowingRequest>> GetRequestsByUserIdAsync(Guid userId);
        Task<IEnumerable<BookBorrowingRequest>> GetAllRequestsAsync();
    }
}
