using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bookify.API.Data;
using Bookify.API.DTO.Booking;
using Bookify.API.Models;
using Bookify.API.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Bookify.API.Services.Concentre
{
    public class BookingService : IBookingService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        public BookingService(DataContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;

        }
        public async Task<bool> Create(BookingForCreateDto booking)
        {
            var bookingToAdd = _mapper.Map<Booking>(booking);

            if (bookingToAdd != null)
            {
                await _db.Bookings.AddAsync(bookingToAdd);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var bookingToDelete = await _db.Bookings.FirstOrDefaultAsync(x => x.BookingId == id);

            if (bookingToDelete == null)
            {
                return false;
            }

            _db.Bookings.Remove(bookingToDelete);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(BookingForEditDto booking)
        {
            var objectToEdit = _mapper.Map<Booking>(booking);
            _db.Bookings.Update(objectToEdit);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<BookingDto>> GetAll()
        {
            var bookings = await _db.Bookings
            .Include(c => c.Client)
            .Include(c => c.House)
            .ToListAsync();

            if (bookings == null)
            {
                var bookingsEmpty = new List<BookingDto>();
                return bookingsEmpty;
            }

            var bookingsToReturn = _mapper.Map<List<BookingDto>>(bookings);

            return bookingsToReturn;
        }

        public async Task<BookingDto> GetById(int id)
        {
            var booking = await _db.Bookings
            .Include(c => c.Client)
            .Include(c => c.House)
            .FirstOrDefaultAsync(x => x.ClientId == id);

            var bookingToReturn = _mapper.Map<BookingDto>(booking);
            return bookingToReturn;
        }
    }
}