using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class PlatingDetail : FoodDetail
    {
        public int PlatingId { get; set; }

        public List<RecipeListItem> Recipes { get; set; }
    }
}
