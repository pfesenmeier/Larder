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
    public class ActionController : Controller
    {
        // GET: Action
        public ActionResult Index()
        {
            var service = CreateActionService();
            var model = service.GetActions();

            return View(model);
        }

        public ActionResult Create(int id)
        {
            var model = new ActionCreate()
            {
                LarderID = id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActionCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateActionService();
            if (service.CreateAction(model))
            {
                TempData["SaveResult"] = "Step was created.";
                return RedirectToAction("Create", model.LarderID);
            }
            else
            {
                ModelState.AddModelError("", "Step could not be created.");
                return View(model);
            }
        }

        public ActionResult Details(int id)
        {
            var service = CreateActionService();
            var model = service.GetActionById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateActionService();
            var detail = service.GetActionById(id);
            var model =
                new ActionEdit()
                {
                    ID = detail.ID,
                    Description = detail.Description
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ActionEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            else if (model.ID != id)
            {
                ModelState.AddModelError("", "Id mismatch");
                return View(model);
            }
            var service = CreateActionService();
            if (service.UpdateAction(model))
            {
                TempData["Save Result"] = "Your step was updated.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Your step could not be updated.");
                return View(model);
            }
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateActionService();
            var model = service.GetActionById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateActionService();
            service.DeleteAction(id);
            TempData["SaveResult"] = "Your action was deleted";
            return RedirectToAction("Index");
        }

        private ActionService CreateActionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new ActionService(userId);
        }
    }
}