using Larder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models.Ingredient
{
    public class IngredientListItem : FoodListItem
    {
        public int? RecipeId { get; set; }
        public int? LarderId { get; set; }
        public decimal? Amount { get; set; }
        public Unit? Unit { get; set; }
        public override string ToString()
        {
            return $"{Amount} {Unit} {Name}";
        }
    }
}
