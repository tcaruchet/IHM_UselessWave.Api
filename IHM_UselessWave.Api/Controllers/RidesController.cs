using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHM_UselessWave.Api;

namespace IHM_UselessWave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RidesController : ControllerBase
    {
        private readonly dbIHMUselessWaveContext _context;

        public RidesController(dbIHMUselessWaveContext context)
        {
            _context = context;
        }

        // GET: api/Rides
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ride>>> GetRides()
        {
            return await _context.Rides.ToListAsync();
        }

        // GET: api/Rides/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ride>> GetRide(int id)
        {
            var ride = await _context.Rides.FindAsync(id);

            if (ride == null)
            {
                return NotFound();
            }

            return ride;
        }

        // GET: api/Rides/uidUser
        [HttpGet("ByUser/{userUid}")]
        public async Task<ActionResult<IEnumerable<Ride>>> GetRidesByUser(Guid userUid)
        {
            var user = await _context.Users.FindAsync(userUid);

            if (user == null)
            {
                return NotFound();
            }

            List<Ride> rides = await _context.Rides.ToListAsync();

            List<Ride> ridesResult = rides.Where(r => r.UserUid.Equals(userUid)).ToList();


            if (ridesResult == null || ridesResult.Count < 1)
            {
                return NotFound();
            }

            return ridesResult;
        }

        // PUT: api/Rides/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRide(int id, Ride ride)
        {
            if (id != ride.Id)
            {
                return BadRequest();
            }

            _context.Entry(ride).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RideExists(id))
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

        // POST: api/Rides
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ride>> PostRide(Ride ride)
        {
            _context.Rides.Add(ride);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRide", new { id = ride.Id }, ride);
        }

        // DELETE: api/Rides/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ride>> DeleteRide(int id)
        {
            var ride = await _context.Rides.FindAsync(id);
            if (ride == null)
            {
                return NotFound();
            }

            _context.Rides.Remove(ride);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RideExists(int id)
        {
            return _context.Rides.Any(e => e.Id == id);
        }
    }
}
