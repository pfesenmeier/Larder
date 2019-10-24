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
        
        public PlatingDetail GetPlatingbyId(int platingid)
        {
            using (var context = new CookbookContext())
            {
                List<int> RecipeIds =
                        context
                               .RecipePlatings
                               .Where(pr => pr.PlatingID == platingid)
                               .Select(pr => pr.RecipeID)
                               .Distinct()
                               .ToList();
                var service = new RecipeService(userId);
                List<RecipeListItem> Recipes = service.GetRecipeByPlatingId(platingid);
                foreach( var id in RecipeIds)
                {
                    var Recipe =
                                  context
                                         .Recipes
                                         .Where(r => r.ID == id)
                                         .Select(
                                             r =>
                                                 new RecipeListItem
                                                 {
                                                     ID = r.ID,
                                                     Name = r.Name
                                                 }
                                          );
                    Recipes.Add(Recipe);
                }
                //var entity =
                //    context
                //           .Platings
                //           .Single(e => e.ID == id && e.AuthorID == userId);
                //var recipeids =
                //            context
                //                   .RecipePlatings
                //                   .Where(r => r.PlatingID == entity.ID)
                //                   .Select(
                //                       r =>
                //                           new RecipeListItem
                //                           {
                //                               ID = r.RecipeID
                //                           }
                //                   );
                //var recipes =
                //            context
                //                   .Recipes
                //                   .Where(r => r.ID == entity.ID)



                return
                    new PlatingDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name,
                        Description = entity.Description,
                        DateCreated = entity.DateCreated,
                        DateModified = entity.DateModified,
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
                entity.Amount = model.Amount;
                entity.Unit = model.Unit;
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
