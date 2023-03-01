 using CI_Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace CI.Controllers
{
    public class UserController : Controller
    {
        private readonly CIDbContext _CIDbContext;

        public UserController(CIDbContext CIDbContext)
        {
            _CIDbContext = CIDbContext;
        }

        public IActionResult Registration()
        {
            //User user = new User();
            return View();
        }

        [HttpPost]
        public IActionResult Registration(string FirstName, string LastName, int PhoneNumber, string Email, string Password, string ConfirmPassword)
        {
            var obj = _CIDbContext.Users.FirstOrDefault(u => u.Email == Email);

            var userData = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Password = Password,

            };

            if (obj == null)
            {
                _CIDbContext.Users.Add(userData);
                _CIDbContext.SaveChanges();
                return RedirectToAction("Login", "Home");

            }
            else
            {
                ViewBag.RegEmail = "Email Exist";
            }
            return View();
        }


    }
}