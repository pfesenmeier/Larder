using Larder.Models.Ingredient;
using Larder.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// Modify 'Post Create' Route to go back to recipe screen
    // /Recipe/Edit/id=1 something
// Stretch: Modify 'Post Create' Route to go to new create route if button press

// Amount not required- check view
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
        public ActionResult Create(int id, bool isRecipe)
        {
            var model = new IngredientCreate();
            if (isRecipe)
            {
                model.RecipeId = id;
            }
            else
            {
                model.LarderId = id;
            }

            return View(model);
        }
        // POST /Ingredient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IngredientCreate model)
        {
            if ((!ModelState.IsValid) ||
                (SaveCreate(model) == false)) return View(model);
            var isRecipe = IsRecipe(model);
            if (isRecipe == true)
            {
                return RedirectToAction("Create", new { id = model.RecipeId, isRecipe });
            }
            else if (isRecipe == false)
            {
                return RedirectToAction("Create", new { id = model.LarderId, isRecipe });
            }
            else return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAndExit(IngredientCreate model)
        {
            if ((!ModelState.IsValid) ||
                (SaveCreate(model) == false)) return View(model);
            var isRecipe = IsRecipe(model);
            if (isRecipe != null)
            {
                return RedirectToAction("Create", "Action", new { id = model.RecipeId, isRecipe });
            }
            else 
            {
                ModelState.AddModelError("", "Internal Error");
                return View(model); 
            }
        }

        private bool? IsRecipe(IngredientCreate model)
        {
            if (model.RecipeId != null && model.LarderId != null)
            {
                return null;
            }
            else if (model.RecipeId == null && model.LarderId == null)
            {
                
                return null;
            }
            else if (model.RecipeId != null)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        private IngredientService CreateIngredientService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new IngredientService(userId);
        }
    }
}