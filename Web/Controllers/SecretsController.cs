using Microsoft.AspNetCore.Mvc;
using Web.Protos;

namespace Web.Controllers;

[Route("secrets")]
public class SecretsController: Controller {
  private readonly Authentication.AuthenticationClient _authentication;
  private readonly SecretDatabase.SecretDatabaseClient _secretDatabase;

  public SecretsController(Authentication.AuthenticationClient authentication, SecretDatabase.SecretDatabaseClient secretDatabase)
  {
    _authentication = authentication;
    _secretDatabase = secretDatabase;
  }

  [HttpGet]
  public async Task<IActionResult> GetSecrets([FromHeader] string? authorization)
  {
    var response = await _authentication.ValidateTokenAsync(new ValidationRequest()
    {
      Token = authorization ?? ""
    });

    if(!response.Validated) return StatusCode(403);

    var secretsResponse = await _secretDatabase.GetSecretsAsync(new Empty());
    var secrets = new List<string>();

    foreach (var x in secretsResponse.Secrets)
    {
      secrets.Add(x);
    }

    return new OkObjectResult(secrets);
  }
}