using System.Security.Claims;

namespace App.Models;

public static class ClaimsExtensions {
  public static string? FirstClaim(this IEnumerable<Claim>? claims, string type)
  {
    return claims?
      .Where(c => c.Type == type)
      .Select(c => c.Value)
      .FirstOrDefault();
  }

  public static string? AccessToken(this ClaimsPrincipal principal) =>
    principal.Claims.FirstClaim("access_token");

  public static bool LoggedIn(this ClaimsPrincipal principal) =>
    !String.IsNullOrEmpty(principal.AccessToken());
}