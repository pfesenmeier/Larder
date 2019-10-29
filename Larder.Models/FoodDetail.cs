using Larder.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public abstract class FoodDetail : INameDescription
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Date Created")]
        public DateTimeOffset DateCreated { get; set;}
        [Display(Name = "Date Modified")]
        public DateTimeOffset? DateModified { get; set; }
    }
}
