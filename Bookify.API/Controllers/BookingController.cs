using System.Threading.Tasks;
using Bookify.API.DTO.Booking;
using Bookify.API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET api/booking/
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingService.GetAll();

            return Ok(bookings);
        }

        // GET api/booking/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var booking = await _bookingService.GetById(id);

            return Ok(booking);
        }

        // POST api/booking/
        [HttpPost]
        public async Task<IActionResult> Add(BookingForCreateDto bookingForCreate)
        {
            if (bookingForCreate == null)
            {
                return BadRequest("Add object is null");
            }

            var result = await _bookingService.Create(bookingForCreate);

            if (result)
            {
                return Ok("Created successfully");
            }
            else 
            {
                return BadRequest("Failed to Create");
            }
        }

        // POST api/booking/edit
        [HttpPost("edit")]
        public async Task<IActionResult> Edit(BookingForEditDto bookingForEdit)
        {
            var result = await _bookingService.Edit(bookingForEdit);

            if (result)
            {
                return Ok("Edit successful");
            }
            else 
            {
                return BadRequest("Failed to Edit");
            }
        }

        // DELETE api/booking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookingService.Delete(id);

            if (result)
            {
                return Ok("Deleted successfully");
            }
            else 
            {
                return BadRequest("Failed to Delete");
            }
        }
    }
}