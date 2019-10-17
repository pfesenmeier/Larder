using Larder.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Larder.Data.Models
{
    public class RecipePlating: IAuthorID
    {
        //Keys
        public int ID { get; set; }
        public Guid AuthorID { get; set; }
        public int PlatingID { get; set; }
        public int RecipeID { get; set; }

        //Navigation Properties
        public virtual Plating Plating { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}