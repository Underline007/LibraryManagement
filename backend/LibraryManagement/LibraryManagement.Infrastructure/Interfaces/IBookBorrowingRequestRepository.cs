using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using System;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Interfaces
{
    public interface IBookBorrowingRequestRepository : IGenericRepository<BookBorrowingRequest>
    {
        Task UpdateStatusAsync(Guid requestId, BorrowingRequestStatus newStatus);
    }
}
