using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class AuthenticationController: Controller {
  [HttpGet("signin")]
  public IActionResult Signin([FromQuery] string returnUrl)
  {
    return Challenge(new AuthenticationProperties
    {
      RedirectUri = Url.IsLocalUrl(returnUrl) ? returnUrl : "/"
    }, "GitHub");
  }

  [HttpGet("signout")]
  public async Task Signout()
  {
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties()
    {
      RedirectUri = "/"
    });
  }
}