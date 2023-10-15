using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
    {
        private readonly IDiscountRepository _discountrepository;
        private readonly IMapper _mapper;

        public CreateDiscountCommandHandler(IDiscountRepository discountrepository, IMapper mapper)
        {
            _discountrepository = discountrepository;
            _mapper = mapper;
        }

        public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = _mapper.Map<Coupon>(request);
            await _discountrepository.CreateDiscount(coupon);
            var couponModel = _mapper.Map<CouponModel>(coupon);

            return couponModel;
        }
    }
}
