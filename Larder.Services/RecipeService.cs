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
    public class RecipeService
    {
        private readonly Guid userId;
        public RecipeService(Guid userId)
        {
            this.userId = userId;
        }

        public int CreateRecipe(RecipeCreate model)
        {
            using (var context = new CookbookContext())
            {
                var season = new Season()
                {
                    AuthorID = userId,
                    Summer = model.Season.Summer,
                    Fall = model.Season.Fall,
                    Winter = model.Season.Winter,
                    Spring = model.Season.Spring
                };

                context.Seasons.Add(season);
                context.SaveChanges();

                var entity = new Recipe()
                {
                    AuthorID = userId,
                    Name = model.Name,
                    SeasonID = season.ID,
                    Description = model.Description,
                    DateCreated = DateTimeOffset.UtcNow,
                };

                context.Recipes.Add(entity);
                context.SaveChanges();
                return entity.ID;
            }
        }

        public IEnumerable<RecipeListItem> GetRecipes()
        {
            using (var context = new CookbookContext())
            {
                var query =
                    context
                           .Recipes
                           .Where(e => e.AuthorID == userId)
                           .Select(
                               e =>
                                   new RecipeListItem
                                   {
                                       ID = e.ID,
                                       Name = e.Name,
                                       Description = e.Description,
                                       Season = e.Season
                                   }
                           );
                return query.ToArray();
            }
        }

        public RecipeDetail GetRecipebyId(int id)
        {
            using (var context = new CookbookContext())
            {
                var entity =
                    context
                           .Recipes
                           .Single(e => e.ID == id && e.AuthorID == userId);
                var ingredients =
                          context
                                 .Ingredients
                                 .Where(i => i.AuthorID == userId && i.LarderId == id)
                                 .Select(
                                     i =>
                                         new IngredientListItem
                                         {
                                             ID = i.ID,
                                             Amount = i.Amount,
                                             Unit = i.Unit,
                                             Name = i.Name,
                                             Description = i.Description
                                         }
                              );
                var actions =
                      context
                             .Actions
                             .Where(a => a.AuthorID == userId && a.LarderId == id)
                             .Select(
                                 a =>
                                     new ActionListItem
                                     {
                                         ID = a.ID,
                                         Description = a.Description
                                     }
                          );
                return
                    new RecipeDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name,
                        Description = entity.Description,
                        Ingredients = ingredients.ToList(),
                        Actions = actions.ToList(),
                        Seasons = entity.Season.GetSeasons(),
                        DateCreated = entity.DateCreated,
                        DateModified = entity.DateModified
                    };
            }
        }

        public List<RecipeListItem> GetRecipesByPlatingId(int id)
        {

            using (var context = new CookbookContext())
            {
              return context
                            .Recipes
                            .Where(x => x.RecipePlatings.Any(rp => rp.PlatingID == id))
                            .Select(
                                r =>
                                    new RecipeListItem()
                                    {
                                        ID = r.ID,
                                        Name = r.Name
                                    })
                            .ToList();
            }
        }

    public int GetIdbyName(string name)
        {
            using (var context = new CookbookContext())
            {
                var entity =
                     context
                            .Recipes
                            .Single(e => e.Name == name && e.AuthorID == userId);
                return entity.ID;
            }
        }

        public bool UpdateRecipe(RecipeEdit model)
        {
            using (var context = new CookbookContext())
            {
                var entity =
                    context
                           .Recipes
                           .Single(e => e.ID == model.ID && e.AuthorID == userId);
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.DateModified = DateTimeOffset.UtcNow;

                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteRecipe(int id)
        {
            using (var context = new CookbookContext())
            {
                var entity =
                    context
                           .Recipes
                           .Single(e => e.ID == id && e.AuthorID == userId);
                context.Recipes.Remove(entity);

                return context.SaveChanges() == 1;
            }
        }
    }
}
