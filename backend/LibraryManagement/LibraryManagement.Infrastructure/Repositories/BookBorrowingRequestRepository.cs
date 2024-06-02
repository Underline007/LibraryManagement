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

        public async Task UpdateStatusAsync(Guid requestId, BorrowingRequestStatus newStatus)
        {
            var request = await _context.BookBorrowingRequests.FindAsync(requestId);
            if (request == null)
            {
                throw new KeyNotFoundException("Borrowing request not found.");
            }
            request.Status = newStatus;
            _context.BookBorrowingRequests.Update(request);
            await _context.SaveChangesAsync();
        }
    }
}
