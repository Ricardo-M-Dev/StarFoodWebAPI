using MediatR;
using StarFood.Application.Base;
using StarFood.Application.Base.Messages;
using StarFood.Application.DomainModel.Commands;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;

namespace StarFood.Application.Handlers
{
    public class ProductsCommandHandler :
        IRequestHandler<CreateProductCommand, ICommandResponse>,
        IRequestHandler<UpdateProductCommand, ICommandResponse>,
        IRequestHandler<DeleteProductCommand, ICommandResponse>,
        IRequestHandler<StatusProductCommand, ICommandResponse>
    {
        private readonly IProductsRepository _productRepository;
        private readonly IVariationsRepository _variationsRepository;
        private readonly IUnitOfWork<StarFoodDbContext> _unitOfWork;

        public ProductsCommandHandler(
            IProductsRepository productRepository,
            IVariationsRepository variationsRepository,
            IUnitOfWork<StarFoodDbContext> unitOfWork

        )
        {
            _productRepository = productRepository;
            _variationsRepository = variationsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Products? newProduct = new Products
            {
                Name = request.Name,
                Description = request.Description,
                ImgUrl = request.ImgUrl,
                CreatedTime = DateTime.Now,
                CategoryId = request.CategoryId,
                IsAvailable = true,
                Active = true,
                RestaurantId = request.RestaurantId,
            };

            _productRepository.Add(newProduct);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            foreach (var variation in request.Variations)
            {
                var newVariation = new Variations
                {
                    Description = variation.Description,
                    ProductId = newProduct.Id,
                    Value = variation.Value,
                    CreatedTime = DateTime.Now,
                    IsAvailable = true,
                    Active = true,
                    RestaurantId = request.RestaurantId,
                };

                _variationsRepository.Add(newVariation);

            };

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new SuccessCommandResponse(newProduct);
        }

        public async Task<ICommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var updateProduct = _productRepository.GetProductById(request.Restaurant, request.Id);

            if (updateProduct == null)
            {
                return new ErrorCommandResponse();
            }

            updateProduct.Name = request.Name;
            updateProduct.Description = request.Description;
            updateProduct.CategoryId = request.CategoryId;
            updateProduct.ImgUrl = request.ImgUrl;
            updateProduct.UpdateTime = DateTime.Now;
            updateProduct.IsAvailable = request.IsAvailable;

            _productRepository.Edit(updateProduct);

            List<Variations> updatedVariations = new List<Variations>();

            var variationsData = _variationsRepository.GetVariationsByProductId(request.Restaurant, updateProduct.Id);

            foreach (var variation in variationsData)
            {
                var matchingVariation = request.Variations.FirstOrDefault(v => v.Id == variation.Id);

                if (matchingVariation != null)
                {
                    variation.Description = matchingVariation.Description;
                    variation.ProductId = updateProduct.Id;
                    variation.UpdateTime = DateTime.Now;
                    variation.Value = matchingVariation.Value;
                    variation.IsAvailable = matchingVariation.IsAvailable;
                }
                else
                {
                    variation.Active = false;
                }

                _variationsRepository.Edit(variation);

                updatedVariations.Add(variation);
            }

            foreach (var variation in request.Variations)
            {
                var existingVariation = variationsData.FirstOrDefault(v => v.Id == variation.Id);

                if (existingVariation == null)
                {
                    var newVariation = new Variations
                    {
                        Description = variation.Description,
                        ProductId = updateProduct.Id,
                        Value = variation.Value,
                        CreatedTime = DateTime.Now,
                        RestaurantId = request.RestaurantId,
                        IsAvailable = true,
                        Active = true,
                    };

                    _variationsRepository.Add(newVariation);

                    updatedVariations.Add(newVariation);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            updateProduct.Variations.AddRange(updatedVariations);

            return new SuccessCommandResponse(updateProduct);

        }

        public async Task<ICommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var deleteProduct = _productRepository.GetProductById(request.Restaurant, request.Id);

            if (deleteProduct == null)
            {
                return new ErrorCommandResponse();
            }

            deleteProduct.Active = request.Active;

            _productRepository.Edit(deleteProduct);

            await _unitOfWork.SaveChangesAsync();

            return new SuccessCommandResponse(deleteProduct);
        }

        public async Task<ICommandResponse> Handle(StatusProductCommand request, CancellationToken cancellationToken)
        {
            var statusProduct = _productRepository.GetProductById(request.Restaurant, request.Id);

            if (statusProduct == null)
            {
                return new ErrorCommandResponse();
            }

            statusProduct.IsAvailable = request.IsAvailable;

            _productRepository.Edit(statusProduct);

            await _unitOfWork.SaveChangesAsync();

            return new SuccessCommandResponse(statusProduct);
        }
    }
}
