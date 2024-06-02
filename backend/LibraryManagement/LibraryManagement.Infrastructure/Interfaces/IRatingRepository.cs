using LibraryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Interfaces
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        Task<IEnumerable<Rating>> GetRatingsByBookIdAsync(Guid bookId);
        Task<Rating> GetRatingByUserAndBookAsync(Guid userId, Guid bookId);
    }
}
