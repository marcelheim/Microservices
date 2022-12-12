using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class AuthenticationController: Controller {
  [HttpGet("signin")]
  public IActionResult Signin([FromQuery] string returnUrl)
  {
    //return Github Authentication Challenge;
  }

  [HttpGet("signout")]
  public async Task Signout()
  {
    //await Signout;
  }
}