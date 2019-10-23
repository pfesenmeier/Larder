﻿using Larder.Models.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class IngredientCreateFromTemplate : IngredientCreate
    {
        public List<LarderListItem> Templates { get; set; }
    }
}