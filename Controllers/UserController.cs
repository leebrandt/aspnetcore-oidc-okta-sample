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
using Okta.Sdk;
using Okta.Sdk.Configuration;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using AspnetOkta.Mapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AspnetOkta.Controllers
{
  public class UserController : Controller
  {
    private OktaClient client;
    //private string userId;

    public UserController()
    {
      client = new OktaClient(new OktaClientConfiguration
      {
        OrgUrl = "https://dev-613050.oktapreview.com",
        Token = "00irV9xpiSe5upDotwfq6l4wb40JAiL61GdW8ZAV9f"
      });

    }
    public async Task<IActionResult> Index()
    {
      var idClaim = User.FindFirst(x => x.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
      if (idClaim != null)
      {
        var userId = idClaim.Value;
        var user = await client.Users.GetUserAsync(userId);
        var profile = Map.ToMyUser(user);
        return View(profile);
      }
      else
      {
        return View();
      }
    }

    public async Task<IActionResult> Update()
    {
      var customData = new
      {
        Favorites = new[]{
          new{
            EntityType = "Specimen",
            EntityGuid = "d2852531-8764-11e7-8ed2-e540177b168b",
            Title = "Specimen #559",
            Headline = "Parafin Tissue | Unstained Slide",
            MoreInformation = "Added on 8/22/2017, 7:40:20 PM",
            RouteName = "Specimen View"
          }
        }
      };
      var idClaim = User.FindFirst(x => x.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
      if (idClaim != null)
      {
        var userId = idClaim.Value;
        var user = await client.Users.GetUserAsync(userId);
        user.Profile["custom_data"] = JsonConvert.SerializeObject(
          customData,
          new JsonSerializerSettings
          {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
          });
        await user.UpdateAsync();
        var profile = Map.ToMyUser(user);
        return View("Index", profile);
      }
      else
      {
        return View("Index");
      }
    }
  }
}