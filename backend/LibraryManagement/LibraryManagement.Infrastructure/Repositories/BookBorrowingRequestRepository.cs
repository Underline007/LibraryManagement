using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using LibraryManagement.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookBorrowingRequestRepository : GenericRepository<BookBorrowingRequest>, IBookBorrowingRequestRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public BookBorrowingRequestRepository(ApplicationDbContext context, IUserRepository userRepository) : base(context)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<BookBorrowingRequest>> GetRequestsByUserAndDateAsync(Guid userId, DateTime startDate, DateTime endDate)
        {
            return await _context.BookBorrowingRequests
                .Where(request => request.RequestorId == userId && request.RequestDate >= startDate && request.RequestDate <= endDate)
                .ToListAsync();
        }

        public async Task UpdateStatusAsync(Guid requestId, BorrowingRequestStatus newStatus, Guid approverId)
        {
            var request = await _context.BookBorrowingRequests.FindAsync(requestId);
            if (request == null)
            {
                throw new KeyNotFoundException("Borrowing request not found.");
            }

            var approver = await _context.Users.FindAsync(approverId);
            if (approver == null)
            {
                throw new KeyNotFoundException("Approver not found.");
            }

            request.ApproverId = approverId;
            request.Status = newStatus;

            _context.BookBorrowingRequests.Update(request);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<BookBorrowingRequest>> GetRequestsByUserIdAsync(Guid userId)
        {
            return await _context.BookBorrowingRequests
                .Include(request => request.BookBorrowingRequestDetails)
                    .ThenInclude(detail => detail.Book)
                .Where(request => request.RequestorId == userId)
                .ToListAsync();
        }


        public async Task<IEnumerable<BookBorrowingRequest>> GetAllRequestsAsync()
        {
            return await _context.BookBorrowingRequests
                .Include(request => request.BookBorrowingRequestDetails)
                    .ThenInclude(detail => detail.Book)
                .ToListAsync();
        }


    }
}
