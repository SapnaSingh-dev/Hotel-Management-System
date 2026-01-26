using Hotel_Management_System.Interface;
using Hotel_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hotel_Management_System.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginReq req)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    statusCode = 0,
                    message = "Invalid data"
                });
            }

            var result = await _authService.Login(req);

            if (result.StatusCode != 1)
            {
                return Json(new
                {
                    statusCode = 0,
                    message = result.Msg
                });
            }

            // 🔥 Get role from claims
            

            string redirectUrl = "/User/Dashboard";

            if (result.Role == "Admin")
                redirectUrl = "/Admin/Dashboard";
            else if (result.Role == "Manager")
                redirectUrl = "/Manager/Dashboard";

            return Json(new
            {
                statusCode = 1,
                message = "Login successful",
                redirect = redirectUrl
            });
        }


    }
}
