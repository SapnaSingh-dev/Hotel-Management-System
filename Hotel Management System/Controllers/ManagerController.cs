using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Management_System.Controllers
{
    public class ManagerController : BaseController
    {
        [Authorize(Roles ="Manager, Admin")]
        public IActionResult Dashboard()
        {
            ViewBag.UserName = UserName;
            ViewBag.Role = UserRole;
            return View();
        }
    }
}
