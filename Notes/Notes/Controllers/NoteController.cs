using N.DB.Repository.Interfaces;
using N.NHibernate.Repository;
using System.Web;
using System.Web.Mvc;
using N.DB.Models;
using System.Net.Mime;

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

        /// <summary>
        /// Get public notes without my notes
        /// </summary>
        /// <returns>Notes</returns>
        public ActionResult PublicNotes()
        {
            UserId = UserRepository.FindIdByLogin(User.Identity.Name);

            var notesPublic = NoteRepository.GetAllListNotesPublic(UserId);

            return View(notesPublic);
        }

        /// <summary>
        /// Get my notes list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MyNotes()
        {
            UserId = UserRepository.FindIdByLogin(User.Identity.Name);

            var myNotes = NoteRepository.GetAllListMyNotes(UserId);

            return View(myNotes);
        }

        /// <summary>
        /// Get view for browse
        /// </summary>
        /// <param name="model">Model select note</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Note(Note model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            NoteId = model.Id;

            return View(model);
        }

        /// <summary>
        /// Get view for create new note
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult NewNote() => View("Note");

        /// <summary>
        /// Save new note
        /// </summary>
        /// <param name="model">Note model</param>
        /// <param name="upload">Upload file</param>
        /// <returns>Current note view</returns>
        [HttpPost]
        public ActionResult SaveNewNote(Note model, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Note", model);

            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                model.File = fileName;

                upload.SaveAs(Server.MapPath("~/Files/" + fileName));
            }

            model.User = new User() { Id = UserId };

            NoteRepository.Save(model);

            return RedirectToAction("Note", model);
        }

        /// <summary>
        /// Save current note
        /// </summary>
        /// <param name="model">Note model</param>
        /// <returns>Current edit view</returns>
        [HttpPost]
        public ActionResult Save(Note model)
        {
            model.User = new User() { Id = UserId };

            NoteRepository.Save(model);

            return RedirectToAction("Edit", model);
        }

        /// <summary>
        /// Delete note by Mynotes list
        /// </summary>
        /// <param name="id">Note id</param>
        /// <returns></returns>
        public ActionResult Delete(long id)
        {
            NoteRepository.Delete(id);

            return RedirectToAction("MyNotes");
        }

        /// <summary>
        /// Edit note
        /// </summary>
        /// <param name="model">Model view</param>
        /// <returns>Action current view</returns>
        [HttpGet]
        public ActionResult Edit(Note model) => View("Edit", model);

        /// <summary>
        /// Download file with server
        /// </summary>
        /// <param name="nameFile">File name</param>
        /// <returns></returns>
        public ActionResult GetFile(string nameFile)
        {
            if (nameFile != null)
            {
                string file_path = Server.MapPath("~/Files/" + nameFile);
                return File(file_path, MediaTypeNames.Application.Octet, nameFile);
            }

            return RedirectToAction("MyNotes");
        }
    }
}