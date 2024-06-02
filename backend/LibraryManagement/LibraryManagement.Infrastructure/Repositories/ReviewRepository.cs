using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetReviewsByBookIdAsync(Guid bookId)
        {
            return await _context.Reviews.Where(r => r.BookId == bookId).ToListAsync();
        }

        public async Task<Review> GetReviewByUserAndBookAsync(Guid userId, Guid bookId)
        {
            return await _context.Reviews.SingleOrDefaultAsync(r => r.UserId == userId && r.BookId == bookId);
        }
    }
}
