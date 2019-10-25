using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class PlatingAdd
    {
        public int RecipeId { get; set; }
        public Dictionary<PlatingListItem, bool> Platings { get; set; }
    }
}
