using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarFood.Domain.Commands
{
    public class UpdateUserCommand
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Alias { get; set; }
        public Roles Role { get; set; }
        public int RestaurantId { get; set; }
        public bool IsActive { get; set; }
    }
}
