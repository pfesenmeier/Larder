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

        public IEnumerable<RecipeListItem> GetRecipesBySeason(SeasonFilter season)
        {
            var allRecipes = GetRecipes();
            var FilteredRecipes = new List<RecipeListItem>();
            if (season.Fall)
            {
                FilteredRecipes.AddRange(allRecipes.Where(l => l.Season.Fall == true));
            }
            if (season.Spring)
            {
                FilteredRecipes.AddRange(allRecipes.Where(l => l.Season.Spring == true));
            }
            if (season.Summer)
            {
                FilteredRecipes.AddRange(allRecipes.Where(l => l.Season.Summer == true));
            }
            if (season.Winter)
            {
                FilteredRecipes.AddRange(allRecipes.Where(l => l.Season.Winter == true));
            }
            return FilteredRecipes;
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
                              ).ToList();
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
                          ).ToList();
                var platings = GetPlatingsByRecipeId(id);

                return
                    new RecipeDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name,
                        Description = entity.Description,
                        Ingredients = new IngredientList { LarderId=entity.ID, Ingredients = ingredients },
                        Actions = new ActionList { LarderId=entity.ID, Directions = actions },
                        Seasons = entity.Season.GetSeasons(),
                        DateCreated = entity.DateCreated,
                        DateModified = entity.DateModified,
                        Platings = platings
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

        public List<RecipeListItem> GetRecipesByLarderId(int id)
        {
            using (var context = new CookbookContext())
            {
                return context
                                .Recipes
                                .Where(x => x.Ingredients.Any(i => i.TemplateId == id))
                                .Select(
                                        r =>
                                        new RecipeListItem()
                                        {
                                            ID = r.ID,
                                            Name = r.Name,
                                        }).ToList();
            }
        }

        public List<PlatingListItem> GetPlatingsByRecipeId(int id)
        {

            using (var context = new CookbookContext())
            {
                return context
                              .Platings
                              .Where(x => x.RecipePlatings.Any(rp => rp.RecipeID == id))
                              .Select(
                                  p =>
                                      new PlatingListItem()
                                      {
                                          ID = p.ID,
                                          Name = p.Name
                                      })
                              .ToList();
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
                if (model.Season.Fall) { entity.Season.Fall = true;} else { entity.Season.Fall = false; }
                if (model.Season.Spring) { entity.Season.Spring = true;} else { entity.Season.Spring = false; }
                if (model.Season.Summer) { entity.Season.Summer = true;} else { entity.Season.Summer = false; }
                if (model.Season.Winter) { entity.Season.Winter = true;} else { entity.Season.Winter = false; }

                return context.SaveChanges() != 0;
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
