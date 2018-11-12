using N.DB.Repository.Interfaces;
using N.NHibernate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using Notes.Models;
using N.DB.Models;

namespace Notes.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class NoteController : Controller
    {
        INoteRepository NoteRepository;
        IUserRepository UserRepository;

        static long UserId { get; set; }
        static long NoteId { get; set; }
        public NoteController()
        {
            NoteRepository = new NHNoteRepository();
            UserRepository = new NHUserRepository();
        }

        public ActionResult PublicNotes()
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
        public ActionResult Note(Note model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            NoteId = model.Id;

            return View(model);
        }

        [HttpGet]
        public ActionResult NewNote()
        {
            return View("Note", new N.DB.Models.Note());
        }

        [HttpPost]
        public PartialViewResult SaveNote(Note model)
        {
            model.Id = NoteId;
            model.User = new User() { Id = UserId };

            NoteRepository.Save(model);

            return PartialView();
            
        }
        
        public ActionResult Delete(long id)
        {
            //NoteRepository.Delete(id);
            return RedirectToAction("MyNotes");

        }

        /*public PartialViewResult SaveNote(Note model)
        {
            return PartialView();
            /*var result = Calc(model.Name, model.Args1);
        }*/
    }
}