﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Larder.Data.Models
{
    public enum Unit { ea, T, t, g, c, quart, gallon, lb, oz, fluidoz, percent }
    public class Ingredient : Food
    {
        //Keys
        public int RecipeID { get; set; }

        //Properties
        [Column(TypeName = "decimal")]
        public decimal Amount { get; set; }
        public Unit Unit { get; set; }

        //Navigation Objects
        public virtual Recipe Recipe { get; set; }

        //Methods
        public override string ToString() => $"{Amount} {Unit} {Name}";
    }
}