using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Data.Models
{
    public class Larder : Food
    {
        //IDs
        public int SeasonID { get; set; }
        //Navigation Object
        public virtual Season Season { get; set; }
        //Navigation Properties
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        [DisplayName("Steps")]
        public virtual ICollection<Action> Actions { get; set; }

        //Methods
        public override string ToString() => $"{Name}";
    }
}
