using MediatR;
using StarFood.Application.Base;
using StarFood.Application.Base.Messages;
using StarFood.Application.DomainModel.Commands;
using StarFood.Application.Interfaces;
using StarFood.Domain.Entities;
using StarFood.Infrastructure.Data;

namespace StarFood.Application.Handlers
{
    public class TablesCommandHandler :
        IRequestHandler<CreateTableCommand, ICommandResponse>,
        IRequestHandler<StatusTableCommand, ICommandResponse>,
        IRequestHandler<DeleteTableCommand, ICommandResponse>
    {
        private readonly ITablesRepository _tablesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TablesCommandHandler(
            ITablesRepository tablesRepository, 
            IUnitOfWork<StarFoodDbContext> unitOfWork 
        )
        {
            _tablesRepository = tablesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResponse> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Tables? table = new Tables
                {
                    Number = request.Number,
                    Barcode = request.Barcode,
                    CreatedDate = DateTime.Now,
                    Status = true,
                    Deleted = false,
                    RestaurantId = request.RestaurantId,
                };

                _tablesRepository.Add(table);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SuccessCommandResponse(table.Id, table);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }
        }

        public async Task<ICommandResponse> Handle(StatusTableCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Tables? table = _tablesRepository.GetTableById(request.Id, request.RestaurantId);

                if (table == null)
                {
                    return new ErrorCommandResponse();
                }

                table.Status = request.Status;

                _tablesRepository.Add(table);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SuccessCommandResponse(table);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }
            
        }

        public async Task<ICommandResponse> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Tables? table = _tablesRepository.GetTableById(request.Id, request.RestaurantId);

                if (table == null)
                {
                    return new ErrorCommandResponse();
                }

                table.Deleted = request.Deleted;

                _tablesRepository.Add(table);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SuccessCommandResponse(table);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }
        }
    }
}
