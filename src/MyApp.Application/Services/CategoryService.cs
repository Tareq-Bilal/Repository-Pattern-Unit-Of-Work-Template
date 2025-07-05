using AutoMapper;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;

namespace MyApp.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _unitOfWork.Categories.GetCategoriesWithProductsAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _unitOfWork.Categories.GetCategoryWithProductsAsync(id);
        return category == null ? null : _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var category = _mapper.Map<Category>(createCategoryDto);
        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.SaveAsync();

        var createdCategory = await _unitOfWork.Categories.GetCategoryWithProductsAsync(category.Id);
        return _mapper.Map<CategoryDto>(createdCategory!);
    }

    public async Task<CategoryDto> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
    {
        var existingCategory = await _unitOfWork.Categories.GetByIdAsync(updateCategoryDto.Id);
        if (existingCategory == null)
            throw new ArgumentException("Category not found");

        _mapper.Map(updateCategoryDto, existingCategory);
        existingCategory.UpdatedDate = DateTime.UtcNow;
        
        _unitOfWork.Categories.Update(existingCategory);
        await _unitOfWork.SaveAsync();

        var updatedCategory = await _unitOfWork.Categories.GetCategoryWithProductsAsync(existingCategory.Id);
        return _mapper.Map<CategoryDto>(updatedCategory!);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category == null)
            throw new ArgumentException("Category not found");

        category.IsDeleted = true;
        category.UpdatedDate = DateTime.UtcNow;
        
        _unitOfWork.Categories.Update(category);
        await _unitOfWork.SaveAsync();
    }
}

