using Hotel_Management_System.Models;

namespace Hotel_Management_System.Interface
{
    public interface IAuthService
    {
        public Task<LoginRes> Login(LoginReq req);
    }
}
