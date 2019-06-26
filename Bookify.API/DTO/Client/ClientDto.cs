using System.Collections.Generic;
using Bookify.API.DTO.Booking;
using Bookify.API.Models;

namespace Bookify.API.DTO.Client
{
    public class ClientDto
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<BookingDto> Bookings { get; set; }
    }
}