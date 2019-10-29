using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Larder.Services;
using Larder.Models;

namespace Larder.Controllers
{
    [Authorize]
    public class LarderController : Controller
    {

        // GET: Larder
        public ActionResult Index()
        {
            var service = CreateLarderService();
            var model = service.GetLarders();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LarderCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateLarderService();
            int? id = service.CreateLarder(model);
            if (id != null)
            {
                TempData["SaveResult"] = "Your larder recipe was created.";
                return RedirectToAction("Create", "Ingredient", new { isRecipe=false, id });
            }
            else
            {
                ModelState.AddModelError("", "Larder recipe could not be created.");
                return View(model);
            }
        }

        public ActionResult Details(int id)
        {
            var service = CreateLarderService();
            var model = service.GetLarderbyId(id);

            return View(model);
        }


        public ActionResult Edit (int id)
        {
            var service = CreateLarderService();
            var detail = service.GetLarderbyId(id);
            var model =
                         new LarderEdit
                         {
                             ID = detail.ID,
                             Name = detail.Name,
                             Description = detail.Description,
                             Actions = detail.Actions,
                             Ingredients = detail.Ingredients
                         };

            model.Season = new Data.Models.Season();
            if (detail.Seasons.Contains("Spring")) model.Season.Spring = true;
            if (detail.Seasons.Contains("Summer")) model.Season.Summer = true;
            if (detail.Seasons.Contains("Winter")) model.Season.Winter = true;
            if (detail.Seasons.Contains("Fall")) model.Season.Fall = true;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LarderEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            else if (model.ID != id)
            {
                ModelState.AddModelError("", "Id mismatch");
                return View(model);
            }
            var service = CreateLarderService();
            if (service.UpdateLarder(model))
            {
                TempData["Save Result"] = "Your larder recipe was updated.";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Your larder recipe could not be updated.");
                return View(model);
            }
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateLarderService();
            var model = service.GetLarderbyId(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletPost(int id)
        {
            var service = CreateLarderService();
            service.DeleteLarder(id);
            TempData["Save Result"] = "Your larder was deleted.";
            return RedirectToAction("Index");
        }

        

        private LarderService CreateLarderService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new LarderService(userId);
        }
    }
}