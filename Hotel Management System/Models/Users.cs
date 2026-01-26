namespace Hotel_Management_System.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string role { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
    }
    public class LoginReq
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginRes
    {
        public string Email { get; set; }

        public string Msg { get; set; }
        public int StatusCode { get; set; }
        public string Role { get; set; }


    }
}
