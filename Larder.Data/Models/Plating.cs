using Larder.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Larder.Data.Models
{
    public class Plating : Food
    {   
        //Constructor
        public Plating()
        {
            RecipePlatings = new Collection<RecipePlating>();
        }

        //Navigation Property
        public virtual ICollection<RecipePlating> RecipePlatings { get; private set; }

        //Methods
        public override string ToString() => Name;
    }
}