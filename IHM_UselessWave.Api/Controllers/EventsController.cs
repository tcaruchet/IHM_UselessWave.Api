using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHM_UselessWave.Api;
using System.Numerics;

namespace IHM_UselessWave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly dbIHMUselessWaveContext _context;

        public EventsController(dbIHMUselessWaveContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await FindUsersFromEventList();
        }

        

        // GET: api/Events/ByPoint
        [HttpGet("ByPoint")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsNearest(double longitude, double latitude)
        {
            Vector2 point = new Vector2((float)longitude, (float)latitude);
            List<Event> events = await _context.Events.ToListAsync();
            List<Event> eventsR = new List<Event>();
            foreach (Event item in events)
            {
                if (IsEventNearestPosition(new Vector2((float)item.Longitude, (float)item.Latitude), point))
                    eventsR.Add(item);
            }
            return await FindUsersFromEventList(eventsR);
        }

        private async Task<List<Event>> FindUsersFromEventList(List<Event> events = null)
        {
            events = await _context.Events.ToListAsync();
            foreach (Event ev in events)
            {
                if (ev.UserUid != null)
                    ev.UserU = await _context.Users.FindAsync(ev.UserUid);
            }

            return events;
        }

        private Vector2[] CreatePolygoneAroundPoint(Vector2 point)
        {
            List<Vector2> polygoneAroundCurrent = new List<Vector2>();
            polygoneAroundCurrent.Add(new Vector2((float)((float)point.X + 0.00069696826), (float)((float)point.Y + 0.0032618453)));
            polygoneAroundCurrent.Add(new Vector2((float)((float)point.X - 0.00147173293), (float)((float)point.Y + 0.00246791144)));
            polygoneAroundCurrent.Add(new Vector2((float)((float)point.X - 0.00232374421), (float)((float)point.Y - 0.00049324731)));
            polygoneAroundCurrent.Add(new Vector2((float)((float)point.X - 0.00041060858), (float)((float)point.Y - 0.00375481347)));
            polygoneAroundCurrent.Add(new Vector2((float)((float)point.X + 0.00179677951), (float)((float)point.Y - 0.00246735314)));
            polygoneAroundCurrent.Add(new Vector2((float)((float)point.X + 0.00296627491), (float)((float)point.Y + 0.00131992598)));
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

        //private bool IsEventNearestPosition(Vector2[] polygon, Vector2 point)
        //{
        //    int polygonLength = polygon.Length, i = 0;
        //    bool inside = false;
        //    // x, y for tested point.
        //    float pointX = point.X, pointY = point.Y;
        //    // start / end point for the current polygon segment.
        //    float startX, startY, endX, endY;
        //    Vector2 endPoint = polygon[polygonLength - 1];
        //    endX = endPoint.X;
        //    endY = endPoint.Y;
        //    while (i < polygonLength)
        //    {
        //        startX = endX; startY = endY;
        //        endPoint = polygon[i++];
        //        endX = endPoint.X; endY = endPoint.Y;
        //        //
        //        inside ^= (endY > pointY ^ startY > pointY) /* ? pointY inside [startY;endY] segment ? */
        //                  && /* if so, test if it is under the segment */
        //                  ((pointX - endX) < (pointY - endY) * (startX - endX) / (startY - endY));
        //    }
        //    return inside;
        //}

        private bool IsEventNearestPosition(Vector2 EventPos, Vector2 Position)
        {
            bool result = false;
            double distR = 0.0;
            char unit = 'K';
            double rlat1 = Math.PI * EventPos.Y / 180;
            double rlat2 = Math.PI * Position.Y / 180;
            double theta = EventPos.X - Position.X;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case 'K': //Kilometers -> default
                    distR = dist * 1.609344;
                    break;
                case 'N': //Nautical Miles 
                    distR = dist * 0.8684;
                    break;
                case 'M': //Miles
                    distR = dist;
                    break;
            }

            return distR <= 5;
        }
    }
}
