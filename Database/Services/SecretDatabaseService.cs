using Database.Data;
using Database.Protos;
using Grpc.Core;

namespace Database.Services;

public class SecretDatabaseService: SecretDatabase.SecretDatabaseBase {
  private readonly DatabaseContext _database;

  public SecretDatabaseService(DatabaseContext database)
  {
    _database = database;
  }

  public override Task<SecretsResponse> GetSecrets(Empty request, ServerCallContext context)
  {
    var response = new SecretsResponse();

    foreach (var x in _database.Secrets)
    {
      response.Secrets.Add(x.Text);
    }

    return Task.FromResult(response);
  }
}