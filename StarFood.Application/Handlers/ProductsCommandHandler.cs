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
            try
            {
                Products? newProduct = new Products
                {
                    Name = request.Name,
                    Description = request.Description,
                    ImgUrl = request.ImgUrl,
                    CreatedDate = DateTime.Now,
                    CategoryId = request.CategoryId,
                    Deleted = false,
                    Status = true,
                    RestaurantId = request.RestaurantId,
                };

                _productRepository.Add(newProduct);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                foreach (CreateVariations variation in request.Variations)
                {
                    Variations? newVariation = new Variations
                    {
                        Name = variation.Name,
                        ProductId = newProduct.Id,
                        Value = variation.Value,
                        CreatedDate = DateTime.Now,
                        Status = true,
                        Deleted = false,
                        RestaurantId = request.RestaurantId,
                    };

                    _variationsRepository.Add(newVariation);
                };

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SuccessCommandResponse(newProduct.Id, newProduct);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }

        }

        public async Task<ICommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Products? product = _productRepository.GetProductById(request.RestaurantId, request.Id);

                if (product == null)
                {
                    return new ErrorCommandResponse();
                }

                product.Name = request.Name;
                product.Description = request.Description;
                product.ImgUrl = request.ImgUrl;
                product.CategoryId = request.CategoryId;
                product.UpdatedDate = DateTime.Now;

                _productRepository.Edit(product);

                List<Variations> updatedVariations = new List<Variations>();

                //Busca no banco as Variações com o ProductId do request.
                List<Variations>? variations = _variationsRepository.GetVariationsByProductId(request.RestaurantId, product.Id);

                if (variations.Count == 0)
                {
                    return new ErrorCommandResponse();
                }

                foreach (Variations variation in variations)
                {
                    //Verifica se existe alguma Variação do request no banco
                    UpdateVariationCommand? updatedVariation = request.Variations.FirstOrDefault(v => v.Id == variation.Id);

                    //Se sim, faz o edit
                    if (updatedVariation != null)
                    {
                        variation.Name = updatedVariation.Name;
                        variation.ProductId = product.Id;
                        variation.UpdatedDate = DateTime.Now;
                        variation.Value = updatedVariation.Value;
                        variation.Status = true;
                    }
                    else
                    {
                        //Se não, desativa
                        variation.Status = false;
                    }

                    _variationsRepository.Edit(variation);

                    updatedVariations.Add(variation);
                }

                foreach (UpdateVariationCommand variation in request.Variations)
                {
                    //Verifica se a Variação do request existe no banco
                    Variations? existingVariation = variations.FirstOrDefault(v => v.Id == variation.Id);

                    //Se não, cria-se uma nova
                    if (existingVariation == null)
                    {
                        Variations? newVariation = new Variations
                        {
                            Name = variation.Name,
                            ProductId = product.Id,
                            Value = variation.Value,
                            CreatedDate = DateTime.Now,
                            RestaurantId = request.RestaurantId,
                            Status = true,
                        };

                        _variationsRepository.Add(newVariation);

                        updatedVariations.Add(newVariation);
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

        public async Task<ICommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Products? product = _productRepository.GetProductById(request.RestaurantId, request.Id);

                if (product == null)
                {
                    return new ErrorCommandResponse();
                }

                product.Deleted = request.Deleted;
                product.DeletedDate = DateTime.Now;

                _productRepository.Edit(product);

                await _unitOfWork.SaveChangesAsync();

                return new SuccessCommandResponse(product);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }
        }

        public async Task<ICommandResponse> Handle(StatusProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Products? product = _productRepository.GetProductById(request.RestaurantId, request.Id);

                if (product == null)
                {
                    return new ErrorCommandResponse();
                }

                product.Status = request.Status;

                _productRepository.Edit(product);

                await _unitOfWork.SaveChangesAsync();

                return new SuccessCommandResponse(product);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }
        }
    }
}
