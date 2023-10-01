using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarFood.Domain.Commands
{
    public class UpdateProductCategoryCommand
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
