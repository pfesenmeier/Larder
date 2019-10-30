using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class IngredientList
    {
        public int LarderId { get; set; }
        public List<IngredientListItem> Ingredients { get; set; }
    }
}
