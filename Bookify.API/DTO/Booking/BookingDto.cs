using System;
using Bookify.API.DTO.Client;
using Bookify.API.DTO.House;

namespace Bookify.API.DTO.Booking
{
    public class BookingDto
    {
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
        public HouseDto House { get; set; }
        public int? ClientId { get; set; }
        public ClientDto Client { get; set; }
    }
}