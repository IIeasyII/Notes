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
using System.IO;

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
            return View("Note");
        }


        public ActionResult Sort(IEnumerable<Note> models)
        {

            return RedirectToAction("MyNotes");
        }

        [HttpPost]
        public ActionResult SaveNewNote(Note model)
        {
            //if (Request.Files.Count > 0)
            //{
            //    var file = Request.Files[0];

            //    if (file != null && file.ContentLength > 0)
            //    {
            //        var fileName = Path.GetFileName(file.FileName);
            //        var path = Path.Combine(Server.MapPath("~/Resourse/"), fileName);
            //    }
            //}

            model.User = new User() { Id = UserId };

            NoteRepository.Save(model);

            return RedirectToAction("Note", model);

        }

        [HttpPost]
        public ActionResult Save(Note model)
        {
            model.User = new User() { Id = UserId };

            NoteRepository.Save(model);

            return RedirectToAction("Edit", model);

        }
        
        public ActionResult Delete(long id)
        {
            NoteRepository.DeleteNote(id);

            return RedirectToAction("MyNotes");

        }

        [HttpGet]
        public ActionResult Edit(Note model)
        {
            return View("Edit", model);

        }

        /*public PartialViewResult SaveNote(Note model)
        {
            return PartialView();
            /*var result = Calc(model.Name, model.Args1);
        }*/
    }
}