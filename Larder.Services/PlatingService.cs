using Larder.Data.DAL;
using Larder.Data.Models;
using Larder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Services
{
    public class PlatingService
    {
        private readonly Guid userId;
        public PlatingService(Guid userId)
        {
            this.userId = userId;
        }

        public bool AddPlating(PlatingAdd model)
        {
            using(var context = new CookbookContext())
            {
                var recipe =
                     context.Recipes.Single(r => r.ID == model.RecipeId);
                recipe.RecipePlatings = null;
                foreach (var plating in model.Platings)
                {

                    if (plating.Value == true)
                    {
                        recipe.RecipePlatings.Add(
                                        new RecipePlating()
                                        {
                                            PlatingID = plating.Key.ID,
                                            RecipeID = model.RecipeId
                                        }); 
                    }
                }
                return context.SaveChanges() == 1;
            }
        }

        public bool CreatePlating(PlatingCreate model)
        {
            var entity = new Plating()
            {
                AuthorID = userId,
                Name = model.Name,
                Description = model.Description,
                DateCreated = DateTimeOffset.UtcNow,
            };

            using (var context = new CookbookContext())
            {
                context.Platings.Add(entity);
                return context.SaveChanges() == 1;
            }
        }

        public IEnumerable<PlatingListItem> GetPlatings()
        {
            using (var context = new CookbookContext())
            {
                var query =
                    context
                           .Platings
                           .Where(e => e.AuthorID == userId)
                           .Select(
                               e =>
                                   new PlatingListItem
                                   {
                                       ID = e.ID,
                                       Name = e.Name,
                                       Description = e.Description,
                                   }
                           );
                return query.ToArray();
            }
        }
        
        public PlatingDetail GetPlatingsbyRecipeId(int platingid)
        {
            using (var context = new CookbookContext())
            {
                var entity =
                    context
                           .Platings
                           .Single(e => e.ID == platingid && e.AuthorID == userId);
                var service = new RecipeService(userId);
                List<RecipeListItem> Recipes = service.GetRecipesByPlatingId(platingid);

                return
                    new PlatingDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name,
                        Description = entity.Description,
                        DateCreated = entity.DateCreated,
                        DateModified = entity.DateModified,
                        Recipes = Recipes
                    };
            }
        }

        public bool UpdatePlating(PlatingEdit model)
        {
            using (var context = new CookbookContext())
            {
                var entity =
                    context
                           .Platings
                           .Single(e => e.ID == model.ID && e.AuthorID == userId);
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.DateModified = DateTimeOffset.UtcNow;

                return context.SaveChanges() == 1;
            }
        }

        public bool DeletePlating(int id)
        {
            using (var context = new CookbookContext())
            {
                var entity =
                    context
                           .Platings
                           .Single(e => e.ID == id && e.AuthorID == userId);
                context.Platings.Remove(entity);

                return context.SaveChanges() == 1;
            }
        }



    }
}
