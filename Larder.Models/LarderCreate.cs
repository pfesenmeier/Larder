using Larder.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class LarderCreate : FoodCreate
    {
        [Display(Name = "Seasons")]
        public Season Season { get; set; }
    }
}
