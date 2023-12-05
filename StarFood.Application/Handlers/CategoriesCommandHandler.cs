using MediatR;
using StarFood.Application.Base;
using StarFood.Application.Base.Messages;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;

namespace StarFood.Application.Handlers
{
    public class CategoriesCommandHandler :
        IRequestHandler<CreateCategoryCommand, ICommandResponse>,
        IRequestHandler<UpdateCategoryCommand, ICommandResponse>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesCommandHandler(
        ICategoriesRepository categoriesRepository,
        IUnitOfWork unitOfWork
        )
        {
            _categoriesRepository = categoriesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Categories? newCategory = new Categories
            {
                CategoryName = request.CategoryName,
                ImgUrl = request.ImgUrl,
                CreatedTime = DateTime.Now,
                RestaurantId = request.RestaurantId,
            };

            _categoriesRepository.Add(newCategory);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new SuccessCommandResponse(newCategory);
        }

        public async Task<ICommandResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            Categories? updateCategory = _categoriesRepository.GetCategoryById(request.Restaurant, request.Id);
            
            if (updateCategory == null)
            {
                return new ErrorCommandResponse();
            }

            updateCategory.CategoryName = request.CategoryName;
            updateCategory.UpdateTime = DateTime.Now;
            updateCategory.ImgUrl = request.ImgUrl;
            updateCategory.IsAvailable = request.IsAvailable;

            _categoriesRepository.Edit(updateCategory);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new SuccessCommandResponse(updateCategory);
        }
    }


}
