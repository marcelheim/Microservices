using Database.Protos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

//Autorize Annotation []
public class Secrets : PageModel
{
    //Database Client;

    private List<String> _secrets = new();
    public List<String> SecretsList => _secrets;

    //Constructor;

    public void OnGet()
    {
        //fetch secrets;
    }
}