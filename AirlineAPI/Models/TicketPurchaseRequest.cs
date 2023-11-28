namespace AirlineAPI.Models
{
    public class TicketPurchaseRequest
    {
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string PassengerName { get; set; }
        
    }
}
