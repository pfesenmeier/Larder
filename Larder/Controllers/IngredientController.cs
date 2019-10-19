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
        public ActionResult Create() => View();

        // POST /Ingredient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IngredientCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateIngredientService();
            if (service.CreateIngredient(model))
            {
                TempData["SaveResult"] = "Your ingredient was created.";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Ingredient could not be created.");
                return View(model);
            }
        }

        public ActionResult Details(int id)
        {
            var service = CreateIngredientService();
            var model = service.GetIngredientbyId(id);
            
            return View(model);
        }

        private IngredientService CreateIngredientService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new IngredientService(userId);
        }
    }
}