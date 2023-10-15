
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Queries;

public class GetAllProductsQuery : IRequest<Pagination<ProductResponse>>
{
    public CatalogSpecParams catalogSpecParams { get; set; }

	public GetAllProductsQuery(CatalogSpecParams catalogSpecParams)
	{
		this.catalogSpecParams = catalogSpecParams;
	}
}
