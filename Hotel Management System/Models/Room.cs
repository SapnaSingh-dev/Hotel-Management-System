namespace Hotel_Management_System.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomType { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }  
        public bool IsAvaiable { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
