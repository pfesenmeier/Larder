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

        private RecipeService CreateRecipeService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new RecipeService(userId);
        }
    }
}