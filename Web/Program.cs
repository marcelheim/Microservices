using Web.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpcClient<Authentication.AuthenticationClient>(r =>
{
  r.Address = new Uri(builder.Configuration.GetConnectionString("AuthenticationService"));
});
builder.Services.AddGrpcClient<SecretDatabase.SecretDatabaseClient>(r =>
{
  r.Address = new Uri(builder.Configuration.GetConnectionString("DatabaseService"));
});

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
