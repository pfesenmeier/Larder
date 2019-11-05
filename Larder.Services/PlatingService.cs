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

        public bool AddPlating(PlatingAddList model)
        {
            using(var context = new CookbookContext())
            {
                var entities = context.RecipePlatings.Where(e => e.RecipeID == model.RecipeId);
                foreach (var entity in entities)
                {
                    context.RecipePlatings.Remove(entity);
                }
                foreach (var plating in model.Platings)
                {
                    if (plating.IsIncluded == true)
                    {
                        context.Set<RecipePlating>().Add(new RecipePlating
                        {
                            PlatingID = plating.ID,
                            RecipeID = model.RecipeId
                        });
                    }
                }
                return context.SaveChanges() != -1;
            
                
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
        public PlatingDetail GetPlatingbyId(int id)
        {
            using(var context = new CookbookContext())
            {
                var query =
                    context
                           .Platings
                           .Single(e => e.AuthorID == userId && e.ID == id);
                return new PlatingDetail()
                {
                    ID = query.ID,
                    Name = query.Name,
                    Description = query.Description,
                    DateCreated = query.DateCreated,
                    DateModified = query.DateModified,
                    Recipes = GetRecipesByPlatingId(id)
                }; 
            }
        }

        private List<RecipeListItem> GetRecipesByPlatingId(int id)
        {
            using(var context = new CookbookContext())
            {
               return context
                            .RecipePlatings
                            .Where(rp => rp.PlatingID == id)
                            .Select(rp =>
                                    new RecipeListItem()
                                    {
                                        ID = rp.RecipeID,
                                        Name = context
                                                    .Recipes
                                                    .FirstOrDefault(r => r.ID == rp.RecipeID)
                                                    .Name
                                    })
                                    .ToList();
            }
        }
        public PlatingAddList GetPlatingsAddList(int id)
        {
            var allPlatings = GetPlatings();
            var model = new PlatingAddList()
            {
                RecipeId = id,
                Platings = new List<PlatingAdd>()
            };
            foreach (var plating in allPlatings)
            {
                model.Platings.Add(
                                    new PlatingAdd
                                    {
                                        ID = plating.ID,
                                        Name = plating.Name,
                                        Description = plating.Description,
                                        IsIncluded = false
                                    });
            }
            return model;
        }
        public PlatingAddList GetPlatingsUpdateList(int id)
        {
            var allPlatings = GetPlatings();
            var service = new RecipeService(userId);
            var includedPlatings = service.GetPlatingsByRecipeId(id);
            var model = new PlatingAddList()
            {
                RecipeId = id,
                Platings = new List<PlatingAdd>()
            };
            foreach (var plating in allPlatings)
            {
                model.Platings.Add(
                                    new PlatingAdd
                                    {
                                        ID = plating.ID,
                                        Name = plating.Name,
                                        Description = plating.Description
                                    });
            }
            foreach (var plating in includedPlatings)
            {
                if(model.Platings.Any(p => p.ID == plating.ID))
                {
                    model.Platings.Single(p => p.ID == plating.ID).IsIncluded = true;
                }
            }
            return model;
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
