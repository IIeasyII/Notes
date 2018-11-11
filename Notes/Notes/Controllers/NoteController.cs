using N.DB.Repository.Interfaces;
using N.NHibernate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace Notes.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class NoteController : Controller
    {
        INoteRepository NoteRepository;
        IUserRepository UserRepository;

        static long UserId { get; set; }

        public NoteController()
        {
            NoteRepository = new NHNoteRepository();
            UserRepository = new NHUserRepository();
        }

        public ActionResult ListNotes()
        {
            UserId = UserRepository.FindIdByLogin(User.Identity.Name);

            var notesPublic = NoteRepository.GetAllListNotesPublic(UserId);
            
            return View(notesPublic);
        }

        [HttpGet]
        public ActionResult MyNotes()
        {
            UserId = UserRepository.FindIdByLogin(User.Identity.Name);

            var myNotes = NoteRepository.GetAllListMyNotes(UserId);

            return View(myNotes);
        }

        [HttpGet]
        public ActionResult Note()
        {
            return View();
        }
    }
}