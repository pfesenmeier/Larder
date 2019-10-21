using Larder.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Larder.Data.DAL
{
    public class CookbookContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CookbookContext() : base("CookbookContext")
        {
        }
        public DbSet<Models.Action> Actions { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}
