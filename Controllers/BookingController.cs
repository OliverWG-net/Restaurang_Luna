using Microsoft.AspNetCore.Mvc;
using Restaurang_luna.DTOs.Booking.Other;
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookingController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BookingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
