using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Database.Protos;
using Microsoft.AspNetCore.HttpOverrides;
using User.Protos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers();

#region Authentication Services
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(cookieOptions =>
    {
        // set the path for the authentication challenge
        cookieOptions.LoginPath = "/signin";
        // set the path for the sign out
        cookieOptions.LogoutPath = "/signout";
        cookieOptions.Cookie.SameSite = SameSiteMode.Strict;
        cookieOptions.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    })
    .AddGitHub(githubOptions =>
    {
        githubOptions.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
        githubOptions.ClientId = builder.Configuration["Authentication:GitHub:ClientId"];
        githubOptions.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"];
        githubOptions.CallbackPath = "/signin-github";
        githubOptions.Scope.Add("read:user");
        githubOptions.Events.OnCreatingTicket += context =>
        {
            if (context.AccessToken is { })
            {
                context.Identity?.AddClaim(new Claim("access_token", context.AccessToken));
            }

            return Task.CompletedTask;
        };
    });
#endregion

builder.Services.AddGrpcClient<SecretDatabase.SecretDatabaseClient>(grpcClientFactoryOptions =>
{
    grpcClientFactoryOptions.Address = new Uri(builder.Configuration.GetConnectionString("DatabaseService"));
});
builder.Services.AddGrpcClient<UserService.UserServiceClient>(grpcClientFactoryOptions =>
{
    grpcClientFactoryOptions.Address = new Uri(builder.Configuration.GetConnectionString("UserService"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
  ForwardedHeaders = ForwardedHeaders.XForwardedProto
});
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
