using Larder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Larder.Data.Models;
using Larder.Data.DAL;

namespace Larder.Services
{
    public class ActionService
    {
        private readonly Guid userId;
        public ActionService(Guid userId)
        {
            this.userId = userId;
        }

        public bool CreateAction(ActionCreate model)
        {
            var entity = new Data.Models.Action()
            {
                AuthorID = userId,
                Description = model.Description
            };
            using (var context = new CookbookContext())
            {
                context.Actions.Add(entity);
                return context.SaveChanges() == 1;
            }
        }

        public IEnumerable<ActionListItem> GetActions()
        {
            using (var context = new CookbookContext())
            {
                var query =
                    context
                           .Actions
                           .Where(e => e.AuthorID == userId)
                           .Select(
                               e =>
                                   new ActionListItem
                                   {
                                       ID = e.ID,
                                       Description = e.Description
                                   }
                           );
                return query.ToArray();
            }
        }

        public ActionDetail GetActionById(int id)
        {
            using(var context = new CookbookContext())
            {
                var entity =
                     context
                            .Actions
                            .Single(e => e.ID == id && e.AuthorID == userId);
                return
                    new ActionDetail
                    {
                        ID = entity.ID,
                        Description = entity.Description
                    };
            }
        }

        public bool UpdateAction(ActionEdit model)
        {

            using (var context = new CookbookContext())
            {
                var entity =
                        context
                               .Actions
                               .Single(e => e.ID == model.ID && e.AuthorID == userId);
                entity.Description = model.Description;

                return context.SaveChanges() == 1; 
            }
        }

        public bool DeleteAction(int id)
        {
            using (var context = new CookbookContext())
            {
                var entity =
                     context
                            .Actions
                            .Single(e => e.ID == id && e.AuthorID == userId);
                context.Actions.Remove(entity);

                return context.SaveChanges() == 1;
            }
        }
    }
}
