using Larder.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Larder.Models
{
    public class RecipeStep : IAuthorID
    {
        //Keys
        public int ID { get; set; }
        public Guid AuthorID { get; set; }
        public int RecipeID { get; set; }
        //Property
        public string Text { get; set; }

        //Navigation Object
        public virtual Recipe Recipe { get; set; }
    }
}