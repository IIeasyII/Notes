using N.DB.Repository.Interfaces;
using N.NHibernate.Repository;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Notes.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public static long UserId { get; set; }

        IUserRepository UserRepository;

        public AccountController()
        {
            UserRepository = new NHUserRepository();
        }
        
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = UserRepository.LoadByLogin(model.Login);

            if (user == null || user.Password != model.Password)
            {
                ModelState.AddModelError("", "Неверный логин или пароль");

                return View(model);
            }

            FormsAuthentication.SetAuthCookie(user.Login, false);

            return RedirectToAction("MyNotes", "Note");
        }
        
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult Registry()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registry(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserRepository.LoadByLogin(model.Login);

            if (user != null)
            {
                ModelState.AddModelError("", "Пользователь с таким логином уже зарегистрирован");

                return View(model);
            }

            UserRepository.RegistryUser(model.Login, model.Password);

            return RedirectToAction("Login", "Account");
        }
    }
}