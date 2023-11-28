namespace AirlineAPI.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string PassengerName { get; set; }  

        public int NoOfPeople { get; set; }

        
    }

}
