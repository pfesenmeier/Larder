using Larder.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public abstract class FoodCreate : INameDescription
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
