using LoginMVC.DALs;
using LoginMVC.Models;
using LoginMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LoginMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDAL dal = new UserDAL();
                User user = dal.GetUserLogin(model.Username, model.Password);
                if (user != null)
                {
                    HttpContext.Session.SetString("Username", model.Username);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Username or password are incorrect");
            }

            return View(model);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDAL dal = new UserDAL();
                User user = new User();

                user.Username = model.Username;

                User userRegistered = dal.GetUserLogin(user.Username, model.Password);

                if(userRegistered != null)
                {
                    ModelState.AddModelError("", "This user already exists");
                    return View(model);
                }

                dal.CreateUser(user, model.Password);

                User validationUser = dal.GetUserLogin(model.Username, model.Password);

                if(validationUser != null)
                {
                    HttpContext.Session.SetString("Username", user.Username);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Not able to create the user");
            }

            return View(model);
        }
    }
}
