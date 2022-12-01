using Authentication.Protos;
using Grpc.Core;

namespace Authentication.Services;

public class AuthenticationService: Protos.Authentication.AuthenticationBase {
  private readonly UserDatabase.UserDatabaseClient _userDatabase;

  public AuthenticationService(UserDatabase.UserDatabaseClient userDatabase)
  {
    _userDatabase = userDatabase;
  }

  public override Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, ServerCallContext context)
  {
    var userResponse = _userDatabase.GetUser(new GetUserRequest()
    {
      Username = request.Username
    });

    var token = "";

    if(userResponse.Username == request.Username && userResponse.Password == request.Password) token = $"{request.Username};signed";

    return Task.FromResult(new AuthenticationResponse()
    {
      Token = token
    });
  }

  public override Task<ValidationResponse> ValidateToken(ValidationRequest request, ServerCallContext context)
  {
    var tokenSplit = request.Token.Split(";");

    var userResponse = _userDatabase.GetUser(new GetUserRequest()
    {
      Username = tokenSplit.FirstOrDefault()
    });

    var signed = tokenSplit.LastOrDefault() == "signed";
    bool validated = !(!signed || userResponse.Username == "");

    return Task.FromResult(new ValidationResponse()
    {
      Username = signed ? userResponse.Username : "",
      Validated = validated
    });
  }
}