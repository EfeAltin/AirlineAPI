using AirlineAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AirlineAPI.Controllers
{
    using AirlineAPI.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;



    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public TicketController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("query")]
        public IActionResult QueryTicket(DateTime date, string from, string to, int numberOfPeople)
        {
            var flights = _dbContext.Flights
                .Where(f => f.Date.Date == date.Date && f.FromLocation == from && f.ToLocation == to && f.AvailableSeats >= numberOfPeople)
                .OrderBy(f => f.Date)
                .ToList();

            return Ok(new { Flights = flights });
        }




        [HttpPost("buy")]
        [Authorize]
        
        public IActionResult BuyTicket([FromBody] TicketPurchaseRequest request)
        {
            try
            {
                var flight = _dbContext.Flights
                    .FirstOrDefault(f => f.Date == request.Date && f.FromLocation == request.From && f.ToLocation == request.To && f.AvailableSeats > 0);

                if (flight == null)
                {
                    return BadRequest("No available flights for the specified criteria.");
                }

                flight.AvailableSeats--;

                var ticket = new Ticket
                {
                    Date = request.Date,
                    From = request.From,
                    To = request.To,
                    PassengerName = request.PassengerName,
                    NoOfPeople = 1
                };

                _dbContext.Tickets.Add(ticket);
                _dbContext.SaveChanges();

                // Retrieve the newly created ticket from the database
                var bookedTicket = _dbContext.Tickets
                    .Where(t => t.Date == request.Date && t.From == request.From && t.To == request.To && t.PassengerName == request.PassengerName)
                    .FirstOrDefault();

                return Ok(new { Status = "Ticket booked successfully.", FlightDetails = flight, BookedTicket = bookedTicket });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Error saving changes to the database: {ex.InnerException?.Message}");
            }
        }
    }
}

