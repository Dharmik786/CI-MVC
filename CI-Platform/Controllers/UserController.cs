 using CI_Entity.Models;
using CI_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace CI.Controllers
{
    public class UserController : Controller
    {
        private readonly CIDbContext _CIDbContext;

        public UserController(CIDbContext CIDbContext)
        {
            _CIDbContext = CIDbContext;
        }

        //--------------------------------------------------REGISTRATION---------------------------------------------

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
                var user = _CIDbContext.Users.FirstOrDefault(u => u.Email == model.Email);
                if (user == null)
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

                _CIDbContext.PasswordReset.Add(passwordReset);
                _CIDbContext.SaveChanges();

                var resetLink = Url.Action("Reset_Password", "User", new { email = model.Email, token }, Request.Scheme);

                var fromAddress = new MailAddress("gajeravirajpareshbhai@gmail.com", "Viraj Gajera");
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
                    Credentials = new NetworkCredential("gajeravirajpareshbhai@gmail.com", "drbwjzfrmubtveud"),
                    EnableSsl = true
                };
                smtpClient.Send(message);

                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        //----------------------------------------------RESET PASSWORD-------------------------------------------------------------




        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        ////public IActionResult Reset_Password(ResetPassword rsp)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        // Find the user by email                
        ////        var user = _CIDbContext.Users.FirstOrDefault(u => u.Email == rsp.Email);
        ////        if (user == null)
        ////        {
        ////            return RedirectToAction("Reset_Password", "Home");
        ////        }

        ////        // Find the password reset record by email and token
        ////        var passwordReset = _CIDbContext.PasswordReset.FirstOrDefault(u => u.Email == rsp.Email && u.Token == rsp.Token);
        ////        if (passwordReset == null)
        ////        {
        ////            return RedirectToAction("ResetPassword", "ResetPassword");
        ////        }

        ////        // Update the user's password
        ////        user.Password = rsp.Password;
        ////        _CIDbContext.SaveChanges();

        ////        // Remove the password reset record from the database
        ////        _CIDbContext.PasswordReset.Remove(passwordReset);
        ////        _CIDbContext.SaveChanges();

        ////        return RedirectToAction("Login", "Login");
        ////    }

        ////}
        ///
        

        [HttpGet]
        public ActionResult Reset_Password(string email, string token)
        {
            var passwordReset = _CIDbContext.PasswordReset.FirstOrDefault(pr => pr.Email == email && pr.Token == token);
            if (passwordReset == null)
            {
                return RedirectToAction("Login", "Home");
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
        public ActionResult Reset_Password(ResetPass resetPasswordView)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = _CIDbContext.Users.FirstOrDefault(u => u.Email == resetPasswordView.Email);
                if (user == null)
                {
                    return RedirectToAction("Forget", "User");
                }

                // Find the password reset record by email and token
                var passwordReset = _CIDbContext.PasswordReset.FirstOrDefault(pr => pr.Email == resetPasswordView.Email && pr.Token == resetPasswordView.Token);
                if (passwordReset == null)
                {
                    return RedirectToAction("Logn", "Home");
                }

                // Update the user's password
                user.Password = resetPasswordView.Password;
                _CIDbContext.SaveChanges();

            }

            return RedirectToAction("Login","Home");
        }

    }
}
