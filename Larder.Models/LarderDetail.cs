using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class LarderDetail : FoodDetail
    {
        public List<string> Seasons { get; set; }
        public List<IngredientListItem> Ingredients { get; set; }
        public List<ActionListItem> Actions { get; set; }
        public List<RecipeListItem> Recipes { get; set; }
    }
}
