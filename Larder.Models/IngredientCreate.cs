using Larder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class IngredientCreate : FoodCreate
    {
        public int LarderId { get; set; }
        public decimal? Amount { get; set; }
        public Unit? Unit { get; set; }
    }
}
