using Basket.Core.Entities;
using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Commands
{
    public class CreateShoppingCartCommand : IRequest<ShoppingCartResponse>
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }

        public CreateShoppingCartCommand(string userName, List<ShoppingCartItem> items)
        {
            UserName = userName;
            Items = items;
        }
    }
}
