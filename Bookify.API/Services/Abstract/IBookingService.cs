using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.API.DTO.Booking;

namespace Bookify.API.Services.Abstract
{
    public interface IBookingService
    {
        Task<List<BookingDto>> GetAll();
        Task<BookingDto> GetById(int id);
        Task<bool> Create(BookingForCreateDto booking);
        Task<bool> Edit(BookingForEditDto booking);
        Task<bool> Delete(int id);
    }
}