using AutoMapper;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;

namespace MyApp.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.Products.GetProductsWithCategoryAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.Products.GetProductWithCategoryAsync(id);
        return product == null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _unitOfWork.Products.GetProductsByCategoryAsync(categoryId);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
    {
        var product = _mapper.Map<Product>(createProductDto);
        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveAsync();

        var createdProduct = await _unitOfWork.Products.GetProductWithCategoryAsync(product.Id);
        return _mapper.Map<ProductDto>(createdProduct!);
    }

    public async Task<ProductDto> UpdateProductAsync(UpdateProductDto updateProductDto)
    {
        var existingProduct = await _unitOfWork.Products.GetByIdAsync(updateProductDto.Id);
        if (existingProduct == null)
            throw new ArgumentException("Product not found");

        _mapper.Map(updateProductDto, existingProduct);
        existingProduct.UpdatedDate = DateTime.UtcNow;
        
        _unitOfWork.Products.Update(existingProduct);
        await _unitOfWork.SaveAsync();

        var updatedProduct = await _unitOfWork.Products.GetProductWithCategoryAsync(existingProduct.Id);
        return _mapper.Map<ProductDto>(updatedProduct!);
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
            throw new ArgumentException("Product not found");

        product.IsDeleted = true;
        product.UpdatedDate = DateTime.UtcNow;
        
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveAsync();
    }
}

