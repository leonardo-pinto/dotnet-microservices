using Catalog.Application.Commands;
using Catalog.Core.Repositories;
using Catalog.Core.Entities;
using MediatR;
using Catalog.Application.Responses;
using Catalog.Application.Mappers;

namespace Catalog.Application.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    public CreateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = ProductMapper.Mapper.Map<Product>(request);
        if (productEntity is null)
        {
            throw new ApplicationException("Error to create map new product");
        }

        var newProduct = await _productRepository.CreateProduct(productEntity);
        var productResponse = ProductMapper.Mapper.Map<ProductResponse>(newProduct);
        return productResponse;
    }
}
