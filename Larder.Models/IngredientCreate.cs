using Larder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models.Ingredient
{
    public class IngredientCreate : FoodCreate
    {
        public float Amount { get; set; }
        public Unit Unit { get; set; }
    }
}
