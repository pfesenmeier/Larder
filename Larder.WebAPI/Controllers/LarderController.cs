using Larder.Models;
using Larder.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Larder.WebAPI.Controllers
{
    [Authorize]
    public class LarderController : ApiController
    {
        public IHttpActionResult GetAll()
        {
            LarderService larderService = CreateLarderService();
            var larders = larderService.GetLarders();
            return Ok(larders);
        }

        public IHttpActionResult Get(int id)
        {
            LarderService larderService = CreateLarderService();
            var larder = larderService.GetLarderbyId(id);
            return Ok(larder);
        }

        public IHttpActionResult Post(LarderCreate larder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateLarderService();
            service.CreateLarder(larder);
            return Ok();
        }

        public IHttpActionResult Put(LarderEdit larder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateLarderService();
            if (!service.UpdateLarder(larder))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateLarderService();
            if (!service.DeleteLarder(id))
                return InternalServerError();
            return Ok();
        }

        private LarderService CreateLarderService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var noteService = new LarderService(userId);
            return noteService;
        }
    }
}
