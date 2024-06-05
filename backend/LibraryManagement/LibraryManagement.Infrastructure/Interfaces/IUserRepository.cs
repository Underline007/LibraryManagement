using LibraryManagement.Core.Entities;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUserNameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task UpdateAsync(User user);

    }
}
