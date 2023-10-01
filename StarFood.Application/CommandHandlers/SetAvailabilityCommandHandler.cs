using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarFood.Application.CommandHandlers
{
    public class SetAvailabilityCommandHandler : ISetAvailabilityCommand<SetAvailabilityCommand>
    {
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsAvailable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
