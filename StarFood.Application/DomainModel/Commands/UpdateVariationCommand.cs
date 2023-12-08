using StarFood.Application.Base.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarFood.Domain.Commands
{
    public class UpdateVariationCommand : Command<ICommandResponse>
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime UpdateTime { get; set; }
        public int ProductId { get; set; }
        public decimal Value { get; set; }
        public bool IsAvailable { get; set; }
    }
}
