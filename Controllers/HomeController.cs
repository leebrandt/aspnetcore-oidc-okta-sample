using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http;

namespace AspnetOkta.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";

      return View();
    }

    public IActionResult Contact()
    {
      ViewData["Message"] = "Your contact page.";

      return View();
    }

    [Authorize]
    public IActionResult Secure()
    {
      return View();
    }

    public async Task<IActionResult> Login()
    {
      await HttpContext.Authentication.ChallengeAsync();
      return View();
    }
    public async Task<IActionResult> Logout()
    {
      //await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      //await HttpContext.Authentication.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
      return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
      //return RedirectToAction("Index");      
    }
    public IActionResult Error()
    {
      return View();
    }
  }
}
