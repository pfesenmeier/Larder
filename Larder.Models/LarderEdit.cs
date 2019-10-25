using Larder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class LarderEdit : FoodEdit
    {
        public Season Season { get; set; }
        public List<IngredientListItem> Ingredients { get; set; }
        public List<ActionListItem> Actions { get; set; }
    }
}
