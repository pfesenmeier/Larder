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
        public IHttpActionResult Get()
        {
            LarderService larderService = CreateLarderService();
            var larders = larderService.GetLarders();
            return Ok(larders);
        }

        private LarderService CreateLarderService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var noteService = new LarderService(userId);
            return noteService;
        }
    }
}
