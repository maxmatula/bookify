using System.Collections.Generic;

namespace Bookify.API.Models
{
    public class House
    {
        public int HouseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public int Floors { get; set; }
        public decimal Price { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<Equipment> Equipment { get; set; }
    }
}