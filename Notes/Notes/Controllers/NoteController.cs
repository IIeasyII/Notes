using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notes.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class NoteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}