using CS478_EventPlannerProject.Data;
using CS478_EventPlannerProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CS478_EventPlannerProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VenueController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/Venue
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venue>>> GetVenues()
        {
            return await _context.Venues
                .Include(v => v.TimeSlots)
                .Where(v => v.IsActive)
                .ToListAsync();
        }

        // GET: api/Venue/available
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Venue>>> GetAvailableVenues()
        {
            var venues = await _context.Venues
                .Include(v => v.TimeSlots)
                .Where(v => v.IsActive && v.TimeSlots.Any(ts => ts.IsAvailable))
                .ToListAsync();

            return venues;
        }


        // GET: api/Venue/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venue>> GetVenue(int id)
        {
            var venue = await _context.Venues
                .Include(v => v.TimeSlots)
                .FirstOrDefaultAsync(v => v.VenueId == id);

            if (venue == null)
                return NotFound();

            return venue;
        }

        // GET: api/Venue/5/timeslots
        [HttpGet("{id}/timeslots")]
        public async Task<ActionResult<IEnumerable<VenueTimeSlot>>> GetVenueTimeSlots(int id, [FromQuery] DateTime? date = null)
        {
            var query = _context.VenueTimeSlots
                .Where(ts => ts.VenueId == id && ts.IsAvailable);

            if (date.HasValue)
            {
                query = query.Where(ts => ts.SlotDate.Date == date.Value.Date);
            }

            var timeSlots = await query
                .OrderBy(ts => ts.SlotDate)
                .ThenBy(ts => ts.StartTime)
                .ToListAsync();

            return timeSlots;
        }

        // POST: api/Venue/book
        [HttpPost("book")]
        public async Task<ActionResult> BookTimeSlot([FromBody] BookingRequest request)
        {
            var timeSlot = await _context.VenueTimeSlots
                .FirstOrDefaultAsync(ts => ts.TimeSlotId == request.TimeSlotId);

            if (timeSlot == null)
                return NotFound("Time slot not found");

            if (!timeSlot.IsAvailable)
                return BadRequest("Time slot is not available");

            timeSlot.IsAvailable = false;
            timeSlot.BookedEventId = request.EventId;
            timeSlot.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Time slot booked successfully" });
        }

        // POST: api/Venue/cancel
        [HttpPost("cancel")]
        public async Task<ActionResult> CancelBooking([FromBody] CancelBookingRequest request)
        {
            var timeSlot = await _context.VenueTimeSlots
                .FirstOrDefaultAsync(ts => ts.TimeSlotId == request.TimeSlotId);

            if (timeSlot == null)
                return NotFound("Time slot not found");

            timeSlot.IsAvailable = true;
            timeSlot.BookedEventId = null;
            timeSlot.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking cancelled successfully" });
        }
    }

    public class BookingRequest
    {
        public int TimeSlotId { get; set; }
        public int EventId { get; set; }
    }

    public class CancelBookingRequest
    {
        public int TimeSlotId { get; set; }
    }
}