using CI_Entity.Models;
using CI_Entity.ViewModel;
using CI_Platform.Models;
using CI_PlatForm.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Forget = CI_Platform.Models.Forget;

namespace CI_Platform.Areas.User.Controllers
{
    [Area("User")]
    public class UserController : Controller
    {
        private readonly CIDbContext _CIDbContext;
        private readonly IUserInterface _IUser;

        public UserController(CIDbContext CIDbContext, IUserInterface IUser)
        {
            _IUser = IUser;
            _CIDbContext = CIDbContext;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("landingpage", "Landingpage");
        }
        public IActionResult Login()
        {
            var u = HttpContext.Session.GetString("user");
            var userid = Convert.ToInt32(u);
            if (u != null)
            {
                return RedirectToAction("landingpage", "Landingpage", new { userid });
            }
            else
            {
                LoginVM vm = new LoginVM();
                vm.banners = _IUser.GetBanner().Where(e => e.DeletedAt == null).ToList();
                return View(vm);
            }

        }

        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {



            if (model.Email != null || model.Password != null)
            {
                var admin = _IUser.GetAdminDetails(model.Email, model.Password);
                var user = _IUser.Login(model.Email, model.Password);
                var username = model.Email.Split("@")[0];

                if (admin != null)
                {
                    HttpContext.Session.SetInt32("AdminId", Convert.ToInt32(admin.AdminId));
                    return RedirectToAction("Admin", "Admin", new { area = "Admin" });
                }
                else
                {
                    if (user != null)
                    {

                        HttpContext.Session.SetString("userID", username);
                        HttpContext.Session.SetString("user", user.UserId.ToString());
                        HttpContext.Session.SetString("Firstname", user.FirstName);

                        if (user.Avatar != null)
                        {
                            HttpContext.Session.SetString("Avtar", user.Avatar);
                        }

                        return RedirectToAction("landingpage", "Landingpage", new { user.UserId });

                    }
                    else
                    {
                        ViewBag.Email = "email or pass is incorrect";
                    }
                }
            }


            LoginVM vm = new LoginVM();
            vm.banners = _IUser.GetBanner().Where(e => e.DeletedAt == null).ToList();

            //return RedirectToAction("Login", "User");
            return View(vm);

        }

        //--------------------------------------------------REGISTRATION---------------------------------------------

        public IActionResult Registration()
        {
            Registration r = new Registration();
            r.banners = _IUser.GetBanner();
            return View(r);
        }
        [HttpPost]
        public IActionResult Registration(Registration user)
        {

            if (_IUser.Registration(user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.ConfirmPassword) == true)
            {
                //_CIDbContext.Users.Add(user);
                //_CIDbContext.SaveChanges();
                //_IUser.AddUser(user);
                return RedirectToAction("Login", "User");

            }
            else
            {
                ViewBag.RegEmail = "Email Already Exist";
            }

            Registration r = new Registration();
            r.banners = _IUser.GetBanner();
            return View(r);
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
                if (c == null)
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

            return RedirectToAction("Login", "User");
        }

    }
}
