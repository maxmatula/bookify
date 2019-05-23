using System.Collections.Generic;

namespace Bookify.API.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}