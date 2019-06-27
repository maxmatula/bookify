namespace Bookify.API.DTO.House
{
    public class HouseForEditDto
    {
        public int HouseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rooms { get; set; }
        public int MaxPersonCount { get; set; }
        public int Bathrooms { get; set; }
        public int Floors { get; set; }
        public decimal Price { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
    }
}