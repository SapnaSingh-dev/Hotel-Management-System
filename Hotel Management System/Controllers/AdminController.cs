using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Management_System.Controllers
{
    public class AdminController : BaseController
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard()
        {
            ViewBag.UserName = UserName;
            ViewBag.Role = UserRole;
            return View();
        }
    }
}
