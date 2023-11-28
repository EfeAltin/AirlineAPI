namespace AirlineAPI.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string FlightNumber { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public int AvailableSeats { get; set; }
        public decimal Price { get; set; }

    }
}
