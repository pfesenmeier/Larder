using Larder.Models;
using Larder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Larder.Data.DAL;
namespace Larder.Services
{
    public class LarderService
    {
        private readonly Guid userId;
        public LarderService(Guid userId)
        {
            this.userId = userId;
        }

        public int CreateLarder(LarderCreate model)
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

                var entity = new LarderModel()
                {
                    AuthorID = userId,
                    Name = model.Name,
                    SeasonID = season.ID,
                    Description = model.Description,
                    DateCreated = DateTimeOffset.UtcNow,
                };
            
                context.Larders.Add(entity);
                context.SaveChanges();
                return entity.ID;
            }
        }

        public IEnumerable<LarderListItem> GetLarders()
        {
            using (var context = new CookbookContext())
            {
                var query =
                    context
                           .Larders
                           .OfType<LarderModel>()
                           .Where(e => e.AuthorID == userId)
                           .Where(e => !(e is Recipe))
                           .Select(
                               e =>
                                   new LarderListItem
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

        public LarderDetail GetLarderbyId(int id)
        {
            using (var context = new CookbookContext())
            {
                var entity =
                    context
                           .Larders
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
                                         Description = a.Description,
                                         LarderId = a.LarderId
                                     }
                          );
                var recipes =
                      context
                             .Recipes
                             .Where(r => r.AuthorID == userId)
                             .Where(r => r.Ingredients.Any(i => i.TemplateId == id))
                             .Select(
                                 r =>
                                     new RecipeListItem
                                     {
                                         ID = r.ID,
                                         Name = r.Name
                                     }
                              )
                             .ToList();
                return
                    new LarderDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name,
                        Description = entity.Description,
                        Ingredients = ingredients.ToList(),
                        Actions = actions.ToList(),
                        Seasons = entity.Season.GetSeasons(),
                        DateCreated = entity.DateCreated,
                        DateModified = entity.DateModified,
                        Recipes = recipes
                    };
            }
        }

        public bool UpdateLarder(LarderEdit model)
        {
            using (var context = new CookbookContext())
            {
                var entity =
                    context
                           .Larders
                           .Single(e => e.ID == model.ID && e.AuthorID == userId);
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.DateModified = DateTimeOffset.UtcNow;

                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteLarder(int id)
        {
            using (var context = new CookbookContext())
            {
                var entity =
                    context
                           .Larders
                           .Single(e => e.ID == id && e.AuthorID == userId);
                context.Larders.Remove(entity);

                return context.SaveChanges() == 1;
            }
        }
    }
}
