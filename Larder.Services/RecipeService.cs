using Larder.Data.Models;
using Larder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Larder.Services
//{
//    class RecipeService
//    {
//        private readonly Guid userId;
//        public RecipeService(Guid userId)
//        {
//            this.userId = userId;
//        }

//        public bool CreateRecipe(RecipeCreate model)
//        {
//            var entity = new Recipe()
//            {
//                AuthorID = userId,
//                Name = model.Name,
//                Description = model.Description,
//                DateCreated = DateTimeOffset.UtcNow,
//                Amount = model.Amount,
//                Unit = model.Unit
//            };
//            using (var context = new CookbookContext())
//            {
//                context.Recipes.Add(entity);
//                return context.SaveChanges() == 1;
//            }
//        }

//        public IEnumerable<RecipeListItem> GetRecipes()
//        {
//            using (var context = new CookbookContext())
//            {
//                var query =
//                    context
//                           .Recipes
//                           .Where(e => e.AuthorID == userId)
//                           .Select(
//                               e =>
//                                   new RecipeListItem
//                                   {
//                                       ID = e.ID,
//                                       Name = e.Name,
//                                       Description = e.Description,
//                                       Amount = e.Amount,
//                                       Unit = e.Unit
//                                   }
//                           );
//                return query.ToArray();
//            }
//        }

//        public RecipeDetail GetRecipebyId(int id)
//        {
//            using (var context = new CookbookContext())
//            {
//                var entity =
//                    context
//                           .Recipes
//                           .Single(e => e.ID == id && e.AuthorID == userId);
//                return
//                    new RecipeDetail
//                    {
//                        ID = entity.ID,
//                        Name = entity.Name,
//                        Description = entity.Description,
//                        DateCreated = entity.DateCreated,
//                        DateModified = entity.DateModified,
//                        Amount = entity.Amount,
//                        Unit = entity.Unit
//                    };
//            }
//        }

//        public bool UpdateRecipe(RecipeEdit model)
//        {
//            using (var context = new CookbookContext())
//            {
//                var entity =
//                    context
//                           .Recipes
//                           .Single(e => e.ID == model.ID && e.AuthorID == userId);
//                entity.Name = model.Name;
//                entity.Description = model.Description;
//                entity.Amount = model.Amount;
//                entity.Unit = model.Unit;
//                entity.DateModified = DateTimeOffset.UtcNow;

//                return context.SaveChanges() == 1;
//            }
//        }

//        public bool DeleteRecipe(int id)
//        {
//            using (var context = new CookbookContext())
//            {
//                var entity =
//                    context
//                           .Recipes
//                           .Single(e => e.ID == id && e.AuthorID == userId);
//                context.Recipes.Remove(entity);

//                return context.SaveChanges() == 1;
//            }
//        }
//    }
//}
