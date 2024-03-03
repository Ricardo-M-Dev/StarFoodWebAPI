using MediatR;
using StarFood.Application.Base;
using StarFood.Application.Base.Messages;
using StarFood.Domain;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Domain.ViewModels.Orders;
using StarFood.Infrastructure.Data;

namespace StarFood.Application.Handlers
{
    public class OrderCommandHandler : 
        IRequestHandler<CreateOrderCommand, ICommandResponse>,
        IRequestHandler<UpdateOrderCommand, ICommandResponse>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IUnitOfWork<StarFoodDbContext> _unitOfWork;

        public OrderCommandHandler(
            IOrdersRepository ordersRepository,
            IProductOrderRepository productOrderRepository,
            IUnitOfWork<StarFoodDbContext> unitOfWork
        )
        {
            _ordersRepository = ordersRepository;
            _productOrderRepository = productOrderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Orders? newOrder = new Orders
                {
                    Status = OrderStatus.Waiting,
                    UserId = request.UserId,
                    TableId = request.TableId,
                    Paid = false,
                    Active = true,
                    CreatedDate = DateTime.Now,
                    RestaurantId = request.RestaurantId,
                };

                _ordersRepository.Add(newOrder);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                foreach (CreateProductOrder productOrder in request.ProductsOrder)
                {
                    ProductOrder? newProductOrders = new ProductOrder
                    {
                        OrderId = newOrder.Id,
                        VariationId = productOrder.VariationId,
                        Description = productOrder.Description,
                        Quantity = productOrder.Quantity,
                        CreatedDate = DateTime.Now,
                    };

                    _productOrderRepository.Add(newProductOrders);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SuccessCommandResponse(newOrder.Id, newOrder);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }
        }

        public async Task<ICommandResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<ProductOrder> updatedProductsOrder = new List<ProductOrder>();

                List<ProductOrder> productsOrder = _productOrderRepository.GetProductsOrderByOrderId(request.Id, request.RestaurantId);

                if (productsOrder.Count == 0)
                {
                    return new ErrorCommandResponse();
                }

                foreach (ProductOrder productOrder in productsOrder)
                {
                    ProductOrder updatedProductOrder = request.ProductOrders.FirstOrDefault(po => po.Id == productOrder.Id);

                    if (updatedProductOrder != null)
                    {
                        productOrder.VariationId = updatedProductOrder.VariationId;
                        productOrder.Description = updatedProductOrder.Description;
                        productOrder.Quantity = updatedProductOrder.Quantity;
                        productOrder.UpdatedDate = DateTime.Now;
                    }

                    _productOrderRepository.Edit(productOrder);

                    updatedProductsOrder.Add(productOrder);
                }

                foreach (var productOrder in request.ProductOrders)
                {
                    ProductOrder existingProductOrder = productsOrder.FirstOrDefault(po => po.Id == productOrder.Id);

                    if (existingProductOrder == null)
                    {
                        ProductOrder newProductOrder = new ProductOrder
                        {
                            OrderId = productOrder.OrderId,
                            VariationId = productOrder.VariationId,
                            Description = productOrder.Description,
                            Quantity = productOrder.Quantity,
                            CreatedDate = DateTime.Now
                        };

                        _productOrderRepository.Add(newProductOrder);

                        updatedProductsOrder.Add(newProductOrder);
                    }
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SuccessCommandResponse();
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }
        }
    }
}
