using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Repositories;
using Basket.Application.Responses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Handlers
{
    public class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public CreateShoppingCartHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            // call discount service and apply coupons
            var shoppingCart = await _basketRepository.UpdateBasket(new ShoppingCart
            {
                UserName = request.UserName,
                Items = request.Items
            });
            var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
            return shoppingCartResponse;
        }
    }
}
