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
            if (service.CreateLarder(model))
            {
                TempData["SaveResult"] = "Your larder recipe was created.";
                var id = service.GetIdbyName(model.Name);
                TempData["InstructionType"] = "Larder";
                return RedirectToAction($"Ingredient/Create/{id}");
            }
            else
            {
                ModelState.AddModelError("", "Larder recipe could not be created.");
                return View(model);
            }
        }

        private LarderService CreateLarderService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new LarderService(userId);
        }

    }
}