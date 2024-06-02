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
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public RatingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rating>> GetRatingsByBookIdAsync(Guid bookId)
        {
            return await _context.Ratings.Where(r => r.BookId == bookId).ToListAsync();
        }

        public async Task<Rating> GetRatingByUserAndBookAsync(Guid userId, Guid bookId)
        {
            return await _context.Ratings.SingleOrDefaultAsync(r => r.UserId == userId && r.BookId == bookId);
        }
    }
}
