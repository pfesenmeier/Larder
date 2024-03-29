﻿using Larder.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class IngredientDetail : FoodDetail
    {
        public int LarderId { get; set; }
        public decimal? Amount { get; set; }
        public Unit? Unit { get; set; }

        public int? TemplateId { get; set; }
        [Display(Name = "Template Recipe")]
        public string TemplateName { get; set; }
    }
}
