using Larder.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class LarderEdit : FoodEdit
    {
        public Season Season { get; set; }
        public IngredientList Ingredients { get; set; }
        [Display(Name="Directions")]
        public ActionList Actions { get; set; }
    }
}
