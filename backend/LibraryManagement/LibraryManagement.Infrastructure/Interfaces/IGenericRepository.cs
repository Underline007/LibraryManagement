using LibraryManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<PaginatedList<T>> GetAll(int pageNumber, int pageSize);
        Task<T> GetById(Guid id);
        Task Add(T entity);
        Task Delete(Guid id);
        Task Update(T entity);
        Task SaveChangesAsync();

    }
}
