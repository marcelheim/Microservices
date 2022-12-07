using Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using User.Protos;

namespace Web.Pages;

public class IndexModel : PageModel
{
    //User Service;

    //Constructor;

    private  UserInfoResponse _userInfo = new();
    public UserInfoResponse UserInfo => _userInfo;

    public void OnGet()
    {
        //if logged in -> fetch user response;
    }
}
