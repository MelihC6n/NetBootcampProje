namespace NetBootcampProje.Models
{
    public class Reservation : BaseModel
    {
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Client Client { get; set; }
        public Room Room { get; set; }
    }
}