using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Restaurang_luna.DTOs.Booking.AvailabilityDto;
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
        private readonly IAvailabilityService _availabilityService;

        public BookingController(IBookingService bookingService, IAvailabilityService availabilityService)
        {
            _bookingService = bookingService;
            _availabilityService = availabilityService;
        }
        //[Authorize]
        [HttpGet("availability/{year:int}/{month:int}/{day:int}")]
        public async Task<ActionResult<DayAvailabilityDto>> Availability(int year, int month, int day, [FromQuery] int guests, CancellationToken ct)
        {
            var date = new DateOnly(year, month, day);
            var offset = TimeZoneInfo.FindSystemTimeZoneById("Europe/Stockholm")
                                       .GetUtcOffset(new DateTime(year, month, day));
            var dto = await _availabilityService.GetAvailability(date, guests, offset, ct);

            return Ok(dto);
        }
        // GET: api/<BookingController>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BookingListDto>>> Get([FromQuery] BookingBucket bucket, CancellationToken ct = default)
        {
            var now = DateTimeOffset.UtcNow;

            var bookings = await _bookingService.GetListBucket(bucket, now, ct);

            if (bookings is null || bookings.Count == 0)
                return Ok(Array.Empty<BookingListDto>());

            return Ok(bookings);
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
        [Authorize]
        //[HttpPost]
        [Consumes("application/json", "application/*+json")]
        public async Task<ActionResult<BookingListDto>> Create([FromBody] BookingRequestDto dto, CancellationToken ct)
        {
            var now = DateTimeOffset.UtcNow;
            var booking = await _bookingService.CreateBooking(dto, ct);

            if (booking is null)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = booking.BookingId }, booking);
        }

        // PUT api/<BookingController>/5
        [Authorize]
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
        [Authorize]
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
