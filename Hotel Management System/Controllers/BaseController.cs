using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hotel_Management_System.Controllers

{
    [Authorize]
    public class BaseController : Controller
    {
        protected int UserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        protected string UserName => User.FindFirst(ClaimTypes.Name)!.Value;
        protected string UserRole => User.FindFirst(ClaimTypes.Role)!.Value;
    }
}
