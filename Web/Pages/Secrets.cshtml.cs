using Database.Protos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

[Authorize]
public class Secrets : PageModel
{
    private readonly SecretDatabase.SecretDatabaseClient _secretDatabase;
    private List<String> _secrets = new();
    
    public List<String> SecretsList => _secrets;

    public Secrets(SecretDatabase.SecretDatabaseClient secretDatabase)
    {
        _secretDatabase = secretDatabase;
    }

    public void OnGet()
    {
        var secretsResponse = _secretDatabase.GetSecrets(new Empty());

        foreach (var x in secretsResponse.Secrets)
        {
            _secrets.Add(x);
        }
    }
}