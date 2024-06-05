
namespace LibraryManagement.Infrastructure.Repositories
{
    public class PaginatedList<T> where T : class
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
    }
}