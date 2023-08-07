using com.portfolio.website.Controllers.VM;
using com.portfolio.website.Data;
using com.portfolio.website.Models;
using com.portfolio.website.Utilities;
using Microsoft.AspNetCore.Mvc;
using com.portfolio.website.Utilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace com.portfolio.website.Controllers
{
    public class UserController : Controller
    {
        private readonly comportfoliowebsiteContext _context;

        public UserController(comportfoliowebsiteContext context)
            {
                _context = context;
            }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Signup(SignupVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Password == vm.ConfirmPassword)
                {
                    _context.User.Add(new Models.User()
                    {
                        Email = vm.Email,
                        Password = vm.Password.ToHashString(),

                    });

                    _context.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("ConfirmPassword", "Password do not match");
                    return View(vm);
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var hashPassword = user.Password.ToHashString();
                /*var data= _context.User.Where(x => x.Email == user.Email && x.Password == hashPassword).FirstOrDefault();
                if(data != null)
                 {
                     Request.HttpContext.Session.SetString("IsLoggedIn", "true");
                     return Redirect("/PersonalInformations");
                 }

                 else
                 {
                     ModelState.AddModelError("Password", "Invalid Username or Password");
                 }*/
                addingClaimIdentity(user);
                HttpContext.SetValue("UserId",user.Email);
                return Redirect("/PersonalInformations");
               
            }
           
            return View(user);
        }

        private void addingClaimIdentity(User user)
        {

            var userClaims = new List<Claim>()
            {

                new Claim("UserName", user.Email),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,"user")

            };

            var claimsIdentity = new ClaimsIdentity(
                userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            //httpcontext, current user is authentic user
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)

                );
        }


        public IActionResult Signout()
        {
            HttpContext.SignOutAsync();
            return View(nameof(Login));
        }

    }
}
