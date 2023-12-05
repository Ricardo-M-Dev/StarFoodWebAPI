using StarFood.Application.Base.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarFood.Application.Communication
{
    public interface IMediatorHandler
    {
        Task<ICommandResponse> SendCommand<T>(T comando, CancellationToken cancelation = default(CancellationToken)) where T : Command<ICommandResponse>;
    }
}
