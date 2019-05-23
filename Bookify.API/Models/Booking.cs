using System;

namespace Bookify.API.Models
{
    public class Booking
    {
        public Booking()
        {
            BookingDate = DateTime.Now;
            AdvancePaid = false;
            IsPaid = false;
        }

        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int Adults { get; set; }
        public int Kids { get; set; }
        public int Animals { get; set; }
        public bool AdvancePaid { get; set; }
        public bool IsPaid { get; set; }

        public int? HouseId { get; set; }
        public House House { get; set; }
        public int? ClientId { get; set; }
        public Client Client { get; set; }
    }
}