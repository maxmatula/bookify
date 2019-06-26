using System;

namespace Bookify.API.DTO.Booking
{
    public class BookingForCreateDto
    {
        public DateTime BookingDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int Adults { get; set; }
        public int Kids { get; set; }
        public int Animals { get; set; }
        public bool AdvancePaid { get; set; }
        public bool IsPaid { get; set; }

        public int HouseId { get; set; }
        public int ClientId { get; set; }
    }
}