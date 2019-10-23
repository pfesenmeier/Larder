using Larder.Data.Models;
using Larder.Models.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class IngredientCreateFromTemplate : FoodCreate
    {
        public List<LarderListItem> Templates { get; set; }
        public int RecipeId { get; set; }
        public decimal? Amount { get; set; }
        public Unit? Unit { get; set; }
    }
}
