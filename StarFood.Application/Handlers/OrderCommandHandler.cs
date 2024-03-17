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
        IRequestHandler<UpdateOrderCommand, ICommandResponse>,
        IRequestHandler<StatusOrderCommand, ICommandResponse>,
        IRequestHandler<DeleteOrderCommand, ICommandResponse>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IVariationsRepository _variationsRepository;
        private readonly IUnitOfWork<StarFoodDbContext> _unitOfWork;

        public OrderCommandHandler(
            IOrdersRepository ordersRepository,
            IProductOrderRepository productOrderRepository,
            IVariationsRepository variationsRepository,
            IUnitOfWork<StarFoodDbContext> unitOfWork
        )
        {
            _ordersRepository = ordersRepository;
            _productOrderRepository = productOrderRepository;
            _variationsRepository = variationsRepository;
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
                    Deleted = false,
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
                //Busca no banco os ProductOrder com o OrderId do request.
                List<ProductOrder> productsOrder = _productOrderRepository.GetProductsOrderByOrderId(request.Id, request.RestaurantId);

                if (productsOrder.Count == 0)
                {
                    return new ErrorCommandResponse();
                }

                List<ProductOrder> updatedProductsOrder = new List<ProductOrder>();

                foreach (ProductOrder productOrder in productsOrder)
                {
                    //Verifica no request, se existe o productOrder do banco
                    UpdateProductOrderCommand updatedProductOrder = request.ProductOrders.FirstOrDefault(po => po.Id == productOrder.Id);

                    //Se sim, edita o productOrder
                    if (updatedProductOrder != null)
                    {
                        Variations variation = _variationsRepository.GetVariationById(updatedProductOrder.VariationId, request.RestaurantId);

                        productOrder.VariationId = variation.Id;
                        productOrder.Description = variation.Name;
                        productOrder.Quantity = updatedProductOrder.Quantity;
                        productOrder.UpdatedDate = DateTime.Now;
                    }

                    _productOrderRepository.Edit(productOrder);

                    updatedProductsOrder.Add(productOrder);
                }

                foreach (UpdateProductOrderCommand productOrder in request.ProductOrders)
                {
                    //Verifica no banco, se existe o productOrder do request
                    ProductOrder existingProductOrder = productsOrder.FirstOrDefault(po => po.Id == productOrder.Id);

                    //Se não, cria-se uma nova
                    if (existingProductOrder == null)
                    {
                        Variations variation = _variationsRepository.GetVariationById(productOrder.VariationId, request.RestaurantId);
                        ProductOrder newProductOrder = new ProductOrder
                        {
                            OrderId = request.Id,
                            VariationId = variation.Id,
                            Description = variation.Name,
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

        public async Task<ICommandResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Orders? order = _ordersRepository.GetOrderById(request.Id, request.RestaurantId);

                if (order == null)
                {
                    return new ErrorCommandResponse();
                }

                order.Deleted = request.Deleted;

                _ordersRepository.Edit(order);

                await _unitOfWork.SaveChangesAsync();

                return new SuccessCommandResponse(order);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }
        }

        public async Task<ICommandResponse> Handle(StatusOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Orders? order = _ordersRepository.GetOrderById(request.Id, request.RestaurantId);
                
                if (order == null)
                {
                    return new ErrorCommandResponse();
                }

                order.Status = request.Status;

                _ordersRepository.Edit(order);

                await _unitOfWork.SaveChangesAsync();

                return new SuccessCommandResponse(order);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }
        }
    }
}
