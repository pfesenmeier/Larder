using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Larder.Models
{
    public class Recipe : Food
    {
        //IDs
        public int SeasonID { get; set; }
        //Navigation Object
        public virtual Season Season { get; set; }
        //Navigation Properties
        public virtual ICollection<Ingredient> Ingredients { get; private set; }
        [DisplayName("Plating Styles")]
        public virtual ICollection<RecipePlating> RecipePlatings { get; private set; }
        [DisplayName("Steps")]
        public virtual ICollection<RecipeStep> Actions { get; private set; }

        //Methods
        public override string ToString() => $"{Name}";

        //Constructor (this fixes a CA2227 Warning)
        public Recipe()
        {
            Ingredients = new Collection<Ingredient>();
            RecipePlatings = new Collection<RecipePlating>();
            Actions = new Collection<RecipeStep>();
        }
    }
}