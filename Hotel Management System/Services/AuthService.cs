using Hotel_Management_System.Interface;
using Hotel_Management_System.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Security.Claims;

namespace Hotel_Management_System.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDbConnection dbConnection;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthService(IDbConnection _dbConnection, IHttpContextAccessor _httpContextAccessor)
        {
            dbConnection = _dbConnection;
            httpContextAccessor = _httpContextAccessor;
        }
        public async Task<LoginRes> Login(LoginReq req)
        {
            var res = new LoginRes();
            try
            {
                using (var conn = (SqlConnection)dbConnection)
                {
                    string query = "SELECT Id, Name, Role FROM Users WHERE PasswordHash = @PasswordHash AND Email = @Email";
                    SqlCommand com = new SqlCommand(query, conn);
                    com.Parameters.AddWithValue("@Email", req.Email);
                    com.Parameters.AddWithValue("@PasswordHash", req.Password);
                    conn.Open();
                    SqlDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        String Name = reader["Name"].ToString();
                        String Role = reader["Role"].ToString();
                        int userId = Convert.ToInt32(reader["Id"]);
                        {
                            var claims = new List<Claim>
                        {
                        new Claim(ClaimTypes.Name, Name),
                        new Claim(ClaimTypes.Email, req.Email),
                        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                        new Claim(ClaimTypes.Role, Role)
                        };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            await httpContextAccessor.HttpContext!.SignInAsync
                                (
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity),
                                new AuthenticationProperties
                                {
                                    IsPersistent = true,
                                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                                });
                            res.StatusCode = 1;
                            res.Role = Role;
                            res.Msg = $"Login Successful";

                        }

                    }
                    else
                    {
                        res.StatusCode = 0;
                        res.Msg = "Invalid Email or Password";
                    }
                }
            }
            catch (Exception ex)
            {
                res.StatusCode = -1;
                res.Msg = "Error: " + ex.Message;
            }
            return res;

        }

    }
}
