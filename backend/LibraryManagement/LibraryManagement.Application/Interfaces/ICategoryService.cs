using LibraryManagement.Application.Dtos.Category;
using LibraryManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<PaginatedList<CategoryDto>> GetAllCategoryAsync(int pageNumber, int pageSize);
        Task<CategoryDto>GetCategoryByIdAsync(Guid id);
        Task<CategoryDto>AddCategoryAsync(CategoryCreateEditDto createEditCategoryDto);
        Task DeleteCategoryAsync(Guid id);
        Task UpdateCategoryAsync(Guid id, CategoryCreateEditDto createEditCategoryDto);
    }
}
