namespace WebGUI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CentreId { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string EventName { get; set; }
    }
}
