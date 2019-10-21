using Larder.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Larder.Data.Models
{
    public class Action : IAuthorID
    {
        //Keys
        public int ID { get; set; }
        public Guid AuthorID { get; set; }
        public int? RecipeID { get; set; }
        public int? LarderId { get; set; }
        //Property
        public string Description { get; set; }

        //Navigation Object
        public virtual Recipe Recipe { get; set; }
    }
}