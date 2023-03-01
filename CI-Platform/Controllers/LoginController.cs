﻿using System.Security.Claims;
using CI.Models;
using CI_Entity.Models;
using CI_Platform.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

public class LoginController : Controller
{
    private readonly CIDbContext _CIDbContext;

    public LoginController(CIDbContext CIDbContext)
    { 
        _CIDbContext = CIDbContext;
    }

    //[AllowAnonymous]
    //public IActionResult Login(string returnUrl)
    //{
    //    ViewData["ReturnUrl"] = returnUrl;
    //    return View();
    //}

    //[HttpPost]
    //[AllowAnonymous]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Login(Login model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        //var user = await _CIDbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
    //        var user = await _CIDbContext.Users.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefaultAsync();

    //        if (user != null)
    //        {

    //            return RedirectToAction(nameof(HomeController.landingpage), "Home");
    //        }
    //        else
    //        {
    //            return RedirectToAction(nameof(LoginController.Login), "Home");
    //        }
    //    }
    //    return View();
    //}


    public IActionResult Login()
    {
        return View();
    }


    public async Task<IActionResult>  Login(Login Model)
    {
      //  var obj = await _CIDbContext.Users.FirstOrDefault(u => u.Email == Model.Email && u.Password == Model.Password);
        var user = await _CIDbContext.Users.Where(u => u.Email == Model.Email && u.Password == Model.Password).FirstOrDefaultAsync();


        if (user != null)
        {
            return RedirectToAction(nameof(HomeController.landingpage), "Home");
        }
        else
        {
           ViewBag.Email = "Email or pass is incorrect";
        }

        return View();

    }
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Logout()
    //{
    //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    //    return RedirectToAction(nameof(HomeController.Index), "Home");
    //}
}