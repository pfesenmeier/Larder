﻿using Larder.Data.DAL;
using Larder.Data.Models;
using Larder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Services
{
    public class IngredientService
    {
        private readonly Guid userId;
        public IngredientService(Guid userId)
        {
            this.userId = userId;
        }

        public bool CreateIngredient(IngredientCreate model)
        {
            var entity = new Ingredient()
            {
                AuthorID = userId,
                Name = model.Name,
                Description = model.Description,
                DateCreated = DateTimeOffset.UtcNow,
                Amount = model.Amount,
                Unit = model.Unit,
                LarderId = model.LarderId,
            };

            using (var context = new CookbookContext())
            {
                context.Ingredients.Add(entity);
                return context.SaveChanges() == 1;
            }
        }

        public bool CreateIngredientFromTemplate(IngredientCreateFromTemplate model)
        {
            var entity = new Ingredient()
            {
                AuthorID = userId,
                Name = model.Name,
                Description = model.Description,
                DateCreated = DateTimeOffset.UtcNow,
                Amount = model.Amount,
                Unit = model.Unit,
                LarderId = model.LarderId,
                TemplateId = model.TemplateId
            };

            using (var context = new CookbookContext())
            {
                context.Ingredients.Add(entity);
                return context.SaveChanges() == 1;
            }
        }

        public IEnumerable<IngredientListItem> GetIngredients()
        {
            using (var context = new CookbookContext())
            {
                var query =
                    context
                           .Ingredients
                           .Where(e => e.AuthorID == userId)
                           .Select(
                               e =>
                                   new IngredientListItem
                                   {
                                       ID = e.ID,
                                       Name = e.Name,
                                       Description = e.Description,
                                       Amount = e.Amount,
                                       Unit = e.Unit
                                   }
                           );
                return query.ToArray();
            }
        }
        
        public IngredientDetail GetIngredientbyId(int id)
        {
            using (var context = new CookbookContext())
            {
                var entity =
                    context
                           .Ingredients
                           .Single(e => e.ID == id && e.AuthorID == userId);
                var ingredientDetail =
                    new IngredientDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name,
                        Description = entity.Description,
                        DateCreated = entity.DateCreated,
                        DateModified = entity.DateModified,
                        Amount = entity.Amount,
                        Unit = entity.Unit,
                        TemplateId = entity.TemplateId,
                    };
                var TemplateName = context.Larders
                                      .SingleOrDefault(l => l.AuthorID == userId && l.ID == entity.TemplateId);
                if (TemplateName != null)
                { 
                    ingredientDetail.TemplateName = TemplateName.Name.ToString();
                }
                return ingredientDetail;
            }
        }

        public bool UpdateIngredient(IngredientEdit model)
        {
            using (var context = new CookbookContext())
            {
                var entity =
                    context
                           .Ingredients
                           .Single(e => e.ID == model.ID && e.AuthorID == userId);
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Amount = model.Amount;
                entity.Unit = model.Unit;
                entity.DateModified = DateTimeOffset.UtcNow;

                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteIngredient(int id)
        {
            using (var context = new CookbookContext())
            {
                var entity =
                    context
                           .Ingredients
                           .Single(e => e.ID == id && e.AuthorID == userId);
                context.Ingredients.Remove(entity);

                return context.SaveChanges() == 1;
            }
        }


    }
}
