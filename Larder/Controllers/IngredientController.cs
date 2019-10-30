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
    public class IngredientController : Controller
    {
        // GET: /Ingredient/
        public ActionResult Index()
        {
            var service = CreateIngredientService();
            var model = service.GetIngredients();

            return View(model);
        }

        // GET /Ingredient/Create
        // Display CreateView for first, and...
        public ActionResult Create(int id)
        {
            var model = new IngredientCreate
            {
                LarderId = id
            };

            return View(model);
        }
        // POST /Ingredient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IngredientCreate model)
        {
            if ((!ModelState.IsValid) ||
                (SaveCreate(model) == false)) return View(model);
            
            return RedirectToAction("Create", new { id = model.LarderId });
        }

        public ActionResult CreateFromTemplate(int recipeid)
        {
            var LarderList = GetLarderList();
            var model = new IngredientCreateFromTemplate()
            {
                LarderId = recipeid,
                Templates = LarderList
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromTemplate(IngredientCreateFromTemplate model)
        {
            if ((!ModelState.IsValid) ||
                (SaveCreate(model) == false)) return View(model);

            return RedirectToAction("CreateFromTemplate", new {recipeId = model.LarderId });
        }

        private bool SaveCreate(IngredientCreate model)
        {
            var service = CreateIngredientService();
            if (service.CreateIngredient(model))
            {
                TempData["SaveResult"] = "Your ingredient was created.";
                return true;
            }
            else
            {
                ModelState.AddModelError("", "Ingredient could not be created.");
                return false;
            }
        }

        public ActionResult Details(int id)
        {
            var service = CreateIngredientService();
            var model = service.GetIngredientbyId(id);
            
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateIngredientService();
            var detail = service.GetIngredientbyId(id);
            var model =
                new IngredientEdit
                {
                    ID = detail.ID,
                    Amount = detail.Amount,
                    Unit = detail.Unit,
                    Name = detail.Name,
                    Description = detail.Description
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IngredientEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            else if (model.ID != id)
            {
                ModelState.AddModelError("", "Id mismatch");
                return View(model);
            }
            var service = CreateIngredientService();
            if (service.UpdateIngredient(model))
            {
                TempData["Save Result"] = "Your ingredient was updated.";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Your ingredient could not be updated.");
                return View(model);
            }
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateIngredientService();
            var model = service.GetIngredientbyId(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateIngredientService();
            service.DeleteIngredient(id);
            TempData["SaveResult"] = "Your ingredient was deleted";
            return RedirectToAction("Index");
        }


        private List<LarderListItem> GetLarderList()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LarderService(userId);
            return service.GetLarders().ToList();
        }
        private IngredientService CreateIngredientService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new IngredientService(userId);
        }
    }
}