using MediatR;
using StarFood.Application.Base.Messages;

namespace StarFood.Application.Communication
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ICommandResponse> SendCommand<T>(T comando, CancellationToken cancelation = default(CancellationToken)) where T : Command<ICommandResponse>
        {
            return await _mediator.Send(comando, cancelation);
        }
    }
}
