using Larder.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Larder.Data.Models
{
    public class Recipe : LarderModel
    {
        //Navigation Properties
        [DisplayName("Plating Styles")]
        public virtual ICollection<RecipePlating> RecipePlatings { get; set; }
        [DisplayName("Ingredients from Larder")]
        public virtual ICollection<LarderModel> Larders { get; set; }
    }
}