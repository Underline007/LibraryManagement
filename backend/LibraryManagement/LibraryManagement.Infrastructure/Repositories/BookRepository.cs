using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> SearchAndSortAsync(string? searchTerm, string? sortBy)
        {
            IQueryable<Book> query = _context.Books;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(b => b.Title.Contains(searchTerm));
            }

            switch (sortBy)
            {
                case "Title":
                    query = query.OrderBy(b => b.Title);    
                    break;
                case "Author":
                    query = query.OrderBy(b => b.Author);
                    break;
                default:
                    query = query.OrderBy(b => b.Title);
                    break;
            }
            return await query.ToListAsync();
        }
    }
}
