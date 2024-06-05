using AutoMapper;
using LibraryManagement.Application.Dtos.Category;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Interfaces;
using LibraryManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;


        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> AddCategoryAsync(CategoryCreateEditDto createEditCategoryDto)
        {
            var cateogry = _mapper.Map<Category>(createEditCategoryDto);
            await _categoryRepository.Add(cateogry);
            return _mapper.Map<CategoryDto>(cateogry);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category is null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            await _categoryRepository.Delete(id);    
        }

        public async Task<PaginatedList<CategoryDto>> GetAllCategoryAsync(int pageNumber, int pageSize)
        {
            var categories = await _categoryRepository.GetAll(pageNumber, pageSize);
            return _mapper.Map<PaginatedList<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            var cateogry = await _categoryRepository.GetById(id);
            return _mapper.Map<CategoryDto>(cateogry);
        }

        public async Task UpdateCategoryAsync(Guid id, CategoryCreateEditDto createEditCategoryDto)
        {
            var category = await _categoryRepository.GetById(id);
            if (category is null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            _mapper.Map(createEditCategoryDto, category);
            await _categoryRepository.Update(category);
        }
    }
}
