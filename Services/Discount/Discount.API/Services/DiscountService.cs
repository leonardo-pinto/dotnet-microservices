using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.API.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IMediator mediator, ILogger<DiscountService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetDiscountQuery(request.ProductName);
            var result = await _mediator.Send(query);
            _logger.LogInformation($"Discount is retrieved for the propduct {request.ProductName} and Amount: ${result.Amount}");
            return result;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var command = new CreateDiscountCommand { 
                ProductName = request.Coupon.ProductName,
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description
            };

            var result = await _mediator.Send(command);
            _logger.LogInformation($"Discount is successfully created for the Product {request.Coupon.ProductName}");
            return result;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var command = new UpdateDiscountCommand
            {
                Id = request.Coupon.Id,
                ProductName = request.Coupon.ProductName,
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description
            };

            var result = await _mediator.Send(command);
            _logger.LogInformation($"Discount is successfully updated for the Product {request.Coupon.ProductName}");
            return result;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var command = new DeleteDiscountCommand(request.ProductName);
            var deleted = await _mediator.Send(command);
            var response = new DeleteDiscountResponse
            {
                Success = deleted,
            };
            return response;
        }
    }
}
