using Larder.Models;
using Larder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Larder.Data.DAL;
using Larder.Models.Ingredient;

namespace Larder.Services
{
    public class myLarderService
    {
        private readonly Guid userId;
        public myLarderService(Guid userId)
        {
            this.userId = userId;
        }

        public bool CreateLarder(LarderCreate model)
        {
            var entity = new LarderModel()
            {
                AuthorID = userId,
                Name = model.Name,
                Description = model.Description,
                DateCreated = DateTimeOffset.UtcNow,
            };
            using (var context = new CookbookContext())
            {
                context.Larders.Add(entity);
                return context.SaveChanges() == 1;
            }
        }

        public IEnumerable<LarderListItem> GetLarders()
        {
            using (var context = new CookbookContext())
            {
                var query =
                    context
                           .Larders
                           .Where(e => e.AuthorID == userId)
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
                                         Description = a.Description
                                     }
                          );
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
                        DateModified = entity.DateModified
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
                entity.Amount = model.Amount;
                entity.Unit = model.Unit;
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
