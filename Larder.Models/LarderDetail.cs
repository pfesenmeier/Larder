using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class LarderDetail : FoodDetail
    {
        [Display(Name="Season")]
        public List<string> Seasons { get; set; }
        public List<IngredientListItem> Ingredients { get; set; }
        [Display(Name="Directions")]
        public List<ActionListItem> Actions { get; set; }
        public List<RecipeListItem> Recipes { get; set; }
    }
}
