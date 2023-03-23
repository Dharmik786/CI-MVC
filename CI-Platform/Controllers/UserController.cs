using CI.Models;
using CI_Entity.Models;
using CI_Platform.Models;
using CI_PlatForm.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Forget = CI_Platform.Models.Forget;

namespace CI.Controllers
{
    public class UserController : Controller
    {
        private readonly CIDbContext _CIDbContext;
        private readonly IUserInterface _IUser;

        public UserController(CIDbContext CIDbContext, IUserInterface IUser)
        {
            _IUser=IUser;
            _CIDbContext = CIDbContext;
        }

        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {

            if (ModelState.IsValid)
            {
                var user =  _IUser.Login(model.Email,model.Password);
                var username = model.Email.Split("@")[0];
                if (user != null)
                {

                    HttpContext.Session.SetString("userID", username);
                    HttpContext.Session.SetString("user", user.UserId.ToString());
                    HttpContext.Session.SetString("Firstname", user.FirstName);

                    return RedirectToAction("landingpage", "Landingpage", new { user.UserId });
                    // return RedirectToAction(nameof(HomeController.landingpage), "Home");
                }
                else
                {
                    ViewBag.Email = "email or pass is incorrect";
                }
            }
            return View();
        }

        //--------------------------------------------------REGISTRATION---------------------------------------------

        public IActionResult Registration()
        {
            //User user = new User();
            return View();
        }
        [HttpPost]
       // public IActionResult Registration(string FirstName, string LastName, int PhoneNumber, string Email, string Password, string ConfirmPassword)
        public IActionResult Registration(User user)
        {
           // var obj = _IUser.Registration(user.Email);

            //var userData = new User
            //{
            //    FirstName = FirstName,
            //    LastName = LastName,
            //    PhoneNumber = PhoneNumber,
            //    Email = Email,
            //    Password = Password,

            //};

            if (_IUser.Registration(user))
            {
                //_CIDbContext.Users.Add(user);
                //_CIDbContext.SaveChanges();
                //_IUser.AddUser(user);
                return RedirectToAction("Login", "User");

            }
            else
            {
                ViewBag.RegEmail = "Email Exist";
            }
            return View();
        }

        //----------------------------------------------FORGET PASSWORD-------------------------------------------------------------

        public IActionResult Forget()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Forget(Forget model)
        {
            if (ModelState.IsValid)
            {
                //var c = _IUser.Forget(model.Email);
                //var user = _CIDbContext.Users.FirstOrDefault(u => u.Email == model.Email);
                var c = _IUser.Forget(model.Email);
                if (c ==null)
                {
                    return RedirectToAction("Forget", "user");
                    //ViewBag.Forget = "Enter Valid Email";
                }

                var token = Guid.NewGuid().ToString(); 

                var passwordReset = new PasswordReset
                {
                    Email = model.Email,
                    Token = token
                };

                //_IUser.AddPassToken(passwordReset.Email, passwordReset.Token);
               _IUser.passwordResets(passwordReset.Email, passwordReset.Token);
               // _CIDbContext.PasswordResets.Add(passwordReset);
               //_CIDbContext.SaveChanges();

                var resetLink = Url.Action("Reset_Password", "User", new { email = model.Email, token }, Request.Scheme);

                var fromAddress = new MailAddress("officehl1881@gmail.com", "Vedant shah");
                var toAddress = new MailAddress(model.Email);
                var subject = "Password reset request";
                var body = $"Hi,<br /><br />Please click on the following link to reset your password:<br /><br /><a href='{resetLink}'>{resetLink}</a>";
               
                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    //Credentials = new NetworkCredential("tatvahl@gmail.com", "dvbexvljnrhcflfw"),
                    Credentials = new NetworkCredential("officehl1881@gmail.com", "vrbxqqayjlbvoihx"),
                    EnableSsl = true
                };
                smtpClient.Send(message);

                return RedirectToAction("Login", "User");
            }

            return View();
        }

        //----------------------------------------------RESET PASSWORD-------------------------------------------------------------
        [HttpGet]
        public IActionResult Reset_Password(string email, string token)
        {
            // var passwordReset = _CIDbContext.PasswordResets.FirstOrDefault(u => u.Email == email && u.Token == token);
            var passwordReset = _IUser.passwordResets(email, token);
            if (passwordReset == null)
            {
                return RedirectToAction("Login", "User");
            }
            // Pass the email and token to the view for resetting the password
            var model = new ResetPass
            {
                Email = email,
                Token = token
            };
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Reset_Password(ResetPass model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                //var user = _CIDbContext.Users.FirstOrDefault(u => u.Email == model.Email);
                var user = _IUser.Forget(model.Email);
                if (user == null)
                {
                    return RedirectToAction("Forget", "User");
                }

                // Find the password reset record by email and token
               // var passwordReset = _CIDbContext.PasswordResets.FirstOrDefault(u => u.Email == model.Email && u.Token == model.Token);
               var passwordReset = _IUser.passwordResets(model.Email, model.Token);
                if (passwordReset == null)
                {
                    return RedirectToAction("Logn", "Home");
                }

                // Update the user's password
                user.Password = model.Password;
                _CIDbContext.SaveChanges();

            }

            return RedirectToAction("Login","User");
        }

    }
}
