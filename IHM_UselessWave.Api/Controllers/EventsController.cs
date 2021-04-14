using IHM_UselessWave.Api.Helpers;
using IHM_UselessWave.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHM_UselessWave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly UselessWaveContext _context;

        public EventsController(UselessWaveContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            List<Event> events = await _context.Events.ToListAsync();
            foreach(Event ev in events){
                if (ev.UserUid != null)
                    ev.User = await _context.Users.FindAsync(ev.UserUid);
            }
            return events;
        }

        // GET: api/Events/ByPoint
        [HttpGet("ByPoint")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsNearest(GPSPoint point)
        {
            
            List<Event> events = await _context.Events.Where(e=>IsEventNearestPosition(CreatePolygoneAroundPoint(point), e.Coordinates)).ToListAsync();
            foreach (Event ev in events)
            {
                if (ev.UserUid != null)
                    ev.User = await _context.Users.FindAsync(ev.UserUid);
            }
            return events;
        }

        private GPSPoint[] CreatePolygoneAroundPoint(GPSPoint point)
        {
            List<GPSPoint> polygoneAroundCurrent = new List<GPSPoint>();
            polygoneAroundCurrent.Add(new GPSPoint(point.Longitude + 0.00069696826, point.Latitude + 0.0032618453));
            polygoneAroundCurrent.Add(new GPSPoint(point.Longitude - 0.00147173293, point.Latitude + 0.00246791144));
            polygoneAroundCurrent.Add(new GPSPoint(point.Longitude - 0.00232374421, point.Latitude - 0.00049324731));
            polygoneAroundCurrent.Add(new GPSPoint(point.Longitude - 0.00041060858, point.Latitude - 0.00375481347));
            polygoneAroundCurrent.Add(new GPSPoint(point.Longitude + 0.00179677951, point.Latitude - 0.00246735314));
            polygoneAroundCurrent.Add(new GPSPoint(point.Longitude + 0.00296627491, point.Latitude + 0.00131992598));
            return polygoneAroundCurrent.ToArray();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }
            if(@event.UserUid != null)
                @event.User = await _context.Users.FindAsync(@event.UserUid);
            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, Event @event)
        {
            if (id != @event.Uid)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.Uid }, @event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(Guid id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return @event;
        }

        private bool EventExists(Guid id)
        {
            return _context.Events.Any(e => e.Uid == id);
        }

        private bool IsEventNearestPosition(GPSPoint[] poly, GPSPoint point)
        {
            double[] vertx = poly.Select(p => p.Longitude).ToArray();
            double[] verty = poly.Select(p => p.Latitude).ToArray();
            int nvert = poly.Count() + 1;
            int i, j, c = 0;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((verty[i] > point.Latitude) != (verty[j] > point.Latitude)) &&
                 (point.Longitude < (vertx[j] - vertx[i]) * (point.Longitude - verty[i]) / (verty[j] - verty[i]) + vertx[i]))
                    c = ~c;
            }
            return c==1;
        }
    }
}
