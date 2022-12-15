using Grpc.Core;
using Octokit;
using User.Protos;

namespace User.Services;

public class UserService : Protos.UserService.UserServiceBase
{
    public override Task<UserInfoResponse> UserInfo(UserTokenRequest request, ServerCallContext context)
    {
        return Task.Run(async () =>
        {
          throw new NotImplementedException();
          //fetch and return user info from Github-API;
          return new UserInfoResponse()
          {
            Name = "GitHubUserName",
            AvatarUrl = "GitHubUserAvatarUrl"
          };
        });
    }
}