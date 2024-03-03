using MediatR;
using StarFood.Application.Base;
using StarFood.Application.Base.Messages;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Infrastructure.Data;

namespace StarFood.Application.Handlers
{
    public class CategoriesCommandHandler :
        IRequestHandler<CreateCategoryCommand, ICommandResponse>,
        IRequestHandler<UpdateCategoryCommand, ICommandResponse>,
        IRequestHandler<DeleteCategoryCommand, ICommandResponse>,
        IRequestHandler<StatusCategoryCommand, ICommandResponse>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IUnitOfWork<StarFoodDbContext> _unitOfWork;

        public CategoriesCommandHandler(
            ICategoriesRepository categoriesRepository,
            IUnitOfWork<StarFoodDbContext> unitOfWork
        )
        {
            _categoriesRepository = categoriesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Categories? category = new Categories
                {
                    Name = request.Name,
                    CreatedDate = DateTime.Now,
                    RestaurantId = request.RestaurantId,
                    Deleted = false,
                    Status = true
                };

                _categoriesRepository.Add(category);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SuccessCommandResponse(category.Id, category);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }

        }

        public async Task<ICommandResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Categories? category = _categoriesRepository.GetCategoryById(request.RestaurantId, request.Id);

                if (category == null)
                {
                    return new ErrorCommandResponse();
                }

                category.Name = request.Name;
                category.UpdatedDate = DateTime.Now;

                _categoriesRepository.Edit(category);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SuccessCommandResponse();
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }

        }

        public async Task<ICommandResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Categories? category = _categoriesRepository.GetCategoryById(request.RestaurantId, request.Id);

                if (category == null)
                {
                    return new ErrorCommandResponse();
                }

                category.Deleted = request.Deleted;
                category.DeletedDate = DateTime.Now;

                _categoriesRepository.Edit(category);

                await _unitOfWork.SaveChangesAsync();

                return new SuccessCommandResponse(category);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }

        }

        public async Task<ICommandResponse> Handle(StatusCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Categories? category = _categoriesRepository.GetCategoryById(request.RestaurantId, request.Id);

                if (category == null)
                {
                    return new ErrorCommandResponse();
                }

                category.Status = request.Status;

                _categoriesRepository.Edit(category);

                await _unitOfWork.SaveChangesAsync();

                return new SuccessCommandResponse(category);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }
        }
    }
}
