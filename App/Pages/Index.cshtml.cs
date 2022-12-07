using App.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using User.Protos;

namespace App.Pages;

public class IndexModel : PageModel
{
    private readonly UserService.UserServiceClient _userService;

    public IndexModel(UserService.UserServiceClient userService)
    {
        _userService = userService;
    }

    private  UserInfoResponse _userInfo = new();
    public UserInfoResponse UserInfo => _userInfo;

    public void OnGet()
    {
        if (HttpContext.User.LoggedIn())
        {
            _userInfo = _userService.UserInfo(new UserTokenRequest()
            {
                Token = HttpContext.User.AccessToken(),
                Username = HttpContext.User.Identity?.Name
            });
        }
    }
}
