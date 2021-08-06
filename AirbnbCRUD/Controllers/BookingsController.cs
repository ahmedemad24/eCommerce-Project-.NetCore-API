using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirbnbCRUD.Model;
using AirbnbCRUD.Services;

namespace AirbnbCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBooking _booking;

        public BookingsController(IBooking booking)
        {
            _booking = booking;
        }

        // GET: api/Bookings
        [HttpGet]
        public ActionResult<IEnumerable<Booking>> GetBookings()
        {
            return _booking.GetAllBookings();
        }

        [HttpGet("person/{id}")]
        public ActionResult<IEnumerable<Booking>> GetBookingsForPerson(int id)
        {
            return _booking.GetAllBookingsByPersonId(id).ToArray();
        }
        [HttpGet("House/{id}")]
        public ActionResult<IEnumerable<Booking>> GetBookingsForHouse(int id)
        {
            return _booking.GetAllBookingByHouseId(id).ToArray();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public ActionResult<Booking> GetBooking(int id)
        {
            var booking = _booking.GetBooking(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutBooking(int id, Booking booking)
        {
            if (id != booking.BookingId)
            {
                return BadRequest();
            }

            try
            {
                _booking.EditBooking(booking);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_booking.BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Booking> PostBooking(Booking booking)
        {
            _booking.CreateBooking(booking);

            return CreatedAtAction("GetBooking", new { id = booking.BookingId }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _booking.GetBooking(id);
            if (booking == null)
            {
                return NotFound();
            }

            _booking.DeleteBooking(id);

            return NoContent();
        }
    }
}
