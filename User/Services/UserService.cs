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
            var client = new GitHubClient(new ProductHeaderValue("test"))
            {
                Credentials = new(request.Token)
            };

            var user = await client.User.Get(request.Username);

            return new UserInfoResponse()
            {
                Name = user.Name,
                AvatarUrl = user.AvatarUrl ?? ""
            };
        });
    }
}