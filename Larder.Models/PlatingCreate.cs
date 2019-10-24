using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class PlatingCreate : FoodCreate
    {
        public int RecipeId { get; set; }
    }
}
