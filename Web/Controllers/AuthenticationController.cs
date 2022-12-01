using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Protos;

namespace Web.Controllers;

[Route("auth")]
public class AuthenticationController: Controller {
  private readonly Authentication.AuthenticationClient _authenticationClient;

  public AuthenticationController(Authentication.AuthenticationClient authenticationClient)
  {
    _authenticationClient = authenticationClient;
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] User user)
  {
    var response = await _authenticationClient.AuthenticateAsync(new AuthenticationRequest()
    {
      Username = user.Username,
      Password = user.Password
    });

    return response.Token != "" ? Ok(response.Token) : StatusCode(403);
  }
}