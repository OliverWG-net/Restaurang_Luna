using Microsoft.AspNetCore.Mvc;
using Restaurang_luna.DTOs.Booking.Other;
using Restaurang_luna.DTOs.Booking.Request;
using Restaurang_luna.Models;
using Restaurang_luna.ServiceInterface.Resturant;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurang_luna.Controllers
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
        // GET: api/<BookingController>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BookingListDto>>> Get([FromQuery] BookingBucket bucket = BookingBucket.Current, CancellationToken ct = default)
        {
            var now = DateTimeOffset.UtcNow;

            var Bookings = await _bookingService.GetListBucket(bucket, now, ct);

            if (Bookings.Count == 0 || Bookings == null )
                return NotFound("No bookings found");

            return Ok(Bookings);
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingListDto>> GetById(Guid id, CancellationToken ct)
        {
            var now = DateTimeOffset.UtcNow;

            var booking = await _bookingService.GetBookingById(id, now, ct);

            if (booking == null)
                return NotFound("No booking found");

            return booking;
        }

        // POST api/<BookingController>
        [HttpPost]
        [Consumes("application/json", "application/*+json")]
        public async Task<ActionResult<BookingListDto>> Create([FromBody] BookingRequestDto dto, CancellationToken ct)
        {
            var now = DateTimeOffset.UtcNow;
            var booking = await _bookingService.CreateBooking(dto, now, ct);

            if (booking is null)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = booking.BookingId }, booking);
        }

        // PUT api/<BookingController>/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] PatchBookingDto dto, CancellationToken ct)
        {
            var success = await _bookingService.PatchBooking(id, dto, ct);

            if (!success)
                return BadRequest("Could not update booking");

            var updated = await GetById(id, ct);
            return Ok(updated);

        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id, CancellationToken ct)
        {
            var success = await _bookingService.DeleteBooking(id, ct);

            if (!success)
                return BadRequest("Could not delete booking");

            return Ok(success);
        }
    }
}
