using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class PlatingAddList
    {
        public int RecipeId { get; set; }
        public List<PlatingAdd> Platings { get; set; }
    }
}
