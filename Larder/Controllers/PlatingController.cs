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
    [Authorize]
    public class PlatingController : Controller
    {
        // GET: /Plating/
        public ActionResult Index()
        {
            var service = CreatePlatingService();
            var model = service.GetPlatings();

            return View(model);
        }

        public ActionResult Add(int id)
        {
            var model = PlatingDictionary(id);
            return View(model);
        }

        private PlatingAdd PlatingDictionary(int id)
        {
            var service = CreatePlatingService();
            var platings = service.GetPlatings();
            var model = new PlatingAdd()
            {
                RecipeId = id,
                Platings = new Dictionary<PlatingListItem, bool>()
            };
            foreach (var plating in platings)
            {
                model.Platings.Add(plating, false);
            }
            return model;
        }

        public ActionResult AddUpdate(int id)
        {
            var recipeService = new RecipeService(Guid.Parse(User.Identity.GetUserId()));
            var platings = recipeService.GetPlatingsByRecipeId(id);

            var service = CreatePlatingService();
            var allPlatings = service.GetPlatings();
            var model = new PlatingAdd()
            {
                RecipeId = id
            };
            foreach (var plating in allPlatings)
            {
                if (platings.Any(p => p.ID == plating.ID))
                {
                    model.Platings.Add(plating, true);
                }
                else
                {
                    model.Platings.Add(plating, false);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdate(PlatingAdd model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreatePlatingService();
            if (service.AddPlating(model)) 
            {
                TempData["Save Result"] = "Changes Saved.";
                return RedirectToAction("Edit", "Recipe", new { id = model.RecipeId });
            }
            else
            {
                ModelState.AddModelError("", "Your plating style could not be update.");
                return View(model);
            }
        }

        

        // GET /Plating/Create
        // Display CreateView for first, and...
        public ActionResult Create()
        {
            return View();
        }
        // POST /Plating/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlatingCreate model)
        {
            if ((!ModelState.IsValid) ||
                (SaveCreate(model) == false)) return View(model);

            return RedirectToAction("Index");
        }

        private bool SaveCreate(PlatingCreate model)
        {
            var service = CreatePlatingService();
            if (service.CreatePlating(model))
            {
                TempData["SaveResult"] = "Your Plating was created.";
                return true;
            }
            else
            {
                ModelState.AddModelError("", "Plating could not be created.");
                return false;
            }
        }

        public ActionResult Details(int id)
        {
            var service = CreatePlatingService();
            var model = service.GetPlatingsbyRecipeId(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreatePlatingService();
            var detail = service.GetPlatingsbyRecipeId(id);
            var model =
                new PlatingEdit
                {
                    ID = detail.ID,
                    Name = detail.Name,
                    Description = detail.Description
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PlatingEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            else if (model.ID != id)
            {
                ModelState.AddModelError("", "Id mismatch");
                return View(model);
            }
            var service = CreatePlatingService();
            if (service.UpdatePlating(model))
            {
                TempData["Save Result"] = "Your Plating was updated.";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Your Plating could not be updated.");
                return View(model);
            }
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreatePlatingService();
            var model = service.GetPlatingsbyRecipeId(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreatePlatingService();
            service.DeletePlating(id);
            TempData["SaveResult"] = "Your Plating was deleted";
            return RedirectToAction("Index");
        }

        private PlatingService CreatePlatingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new PlatingService(userId);
        }
    }
}