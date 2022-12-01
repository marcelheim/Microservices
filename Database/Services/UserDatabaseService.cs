using Database.Data;
using Database.Protos;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Database.Services;

public class UserDatabaseService : UserDatabase.UserDatabaseBase
{
  private readonly DatabaseContext _database;

  public UserDatabaseService(DatabaseContext database)
  {
    _database = database;
  }

  public override async Task<UserResponse> GetUser(GetUserRequest request, ServerCallContext context)
  {
    var user = await _database.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

    return new UserResponse()
    {
      Username = user == null ? "" : user.Username,
      Password = user == null ? "" : user.Password
    };
  }
}
