using N.DB.Repository.Interfaces;
using N.NHibernate.Repository;
using Notes.Models;
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

        /// <summary>
        /// Get login view
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login() => View();

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="model">Login model</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserRepository.LoadByLogin(model.Login);

            if (user == null || user.Password != model.Password)
            {
                ModelState.AddModelError("", "Неверный логин или пароль");

                return View(model);
            }

            FormsAuthentication.SetAuthCookie(user.Login, false);

            return RedirectToAction("MyNotes", "Note");
        }

        /// <summary>
        /// Sign out
        /// </summary>
        /// <returns>Login view</returns>
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Get registry view
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Registry() => View();

        /// <summary>
        /// Registry new user
        /// </summary>
        /// <param name="model">Login model</param>
        /// <returns></returns>
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
                ModelState.AddModelError("", "This user is exist");

                return View(model);
            }

            UserRepository.RegistryUser(model.Login, model.Password);

            return RedirectToAction("Login", "Account");
        }
    }
}