using Larder.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Larder.Data.Models
{
    public abstract class Food : IAuthorID, INameDescription
    {
        //Keys
        public int ID { get; set; }
        [Required]
        public Guid AuthorID { get; set; }

        //Properties
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }

    }
}