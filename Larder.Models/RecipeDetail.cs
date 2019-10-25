using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class RecipeDetail : LarderDetail
    {
        public List<PlatingListItem> Platings { get; set; } 
    }
}
