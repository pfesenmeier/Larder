﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class RecipeListItem : LarderListItem
    {
        public List<PlatingListItem> Platings { get; set; }
    }
}
