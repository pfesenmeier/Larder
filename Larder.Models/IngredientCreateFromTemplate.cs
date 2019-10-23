using Larder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class IngredientCreateFromTemplate : IngredientCreate
    {
        public int TemplateId { get; set; }
        public List<LarderListItem> Templates { get; set; }

        //Its LarderId should be just be recipeid's, but that logic shoubl be handled elsewhere.
    }
}
