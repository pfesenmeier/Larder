using Larder.Models;
using Larder.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Larder.Controllers
{
    public class RecipeController : Controller
    {
        public ActionResult Index()
        {
            var service = CreateRecipeService();
            var model = service.GetRecipes();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipeCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateRecipeService();
            int? id = service.CreateRecipe(model);
            if (id != null)
            {
                TempData["SaveResult"] = "Your recipe was created.";
                return RedirectToAction("Create", "Ingredient", new { isRecipe = false, id });
            }
            else
            {
                ModelState.AddModelError("", "Recipe could not be created.");
                return View(model);
            }
        }

        public ActionResult Details(int id)
        {
            var service = CreateRecipeService();
            var model = service.GetRecipebyId(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateRecipeService();
            var detail = service.GetRecipebyId(id);
            var model =
                         new RecipeEdit
                         {
                             ID = detail.ID,
                             Name = detail.Name,
                             Description = detail.Description,
                             Actions = detail.Actions,
                             Ingredients = detail.Ingredients,
                             Season = new Data.Models.Season()
                         };

            if (detail.Seasons.Contains("Spring")) model.Season.Spring = true;
            if (detail.Seasons.Contains("Summer")) model.Season.Summer = true;
            if (detail.Seasons.Contains("Winter")) model.Season.Winter = true;
            if (detail.Seasons.Contains("Fall")) model.Season.Fall = true;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RecipeEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            else if (model.ID != id)
            {
                ModelState.AddModelError("", "Id mismatch");
                return View(model);
            }
            var service = CreateRecipeService();
            if (service.UpdateRecipe(model))
            {
                TempData["Save Result"] = "Your recipe was updated.";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Your recipe could not be updated.");
                return View(model);
            }
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateRecipeService();
            var model = service.GetRecipebyId(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletPost(int id)
        {
            var service = CreateRecipeService();
            service.DeleteRecipe(id);
            TempData["Save Result"] = "Your recipe was deleted.";
            return RedirectToAction("Index");
        }

        private RecipeService CreateRecipeService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new RecipeService(userId);
        }
    }
}