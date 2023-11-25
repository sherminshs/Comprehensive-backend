using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comprehensive_backend.Models;

namespace Comprehensive_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetiredsController : ControllerBase
    {
        private readonly ComprehensiveDbContext _context;

        public RetiredsController(ComprehensiveDbContext context)
        {
            _context = context;
        }

        // GET: api/Retireds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Retired>>> GetRetireds()
        {
          if (_context.Retireds == null)
          {
              return NotFound();
          }
            return await _context.Retireds.ToListAsync();
        }

        // GET: api/Retireds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Retired>> GetRetired(long id)
        {
          if (_context.Retireds == null)
          {
              return NotFound();
          }
            var retired = await _context.Retireds.FindAsync(id);

            if (retired == null)
            {
                return NotFound();
            }

            return retired;
        }

        // PUT: api/Retireds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRetired(long id, Retired retired)
        {
            if (id != retired.RetiredId)
            {
                return BadRequest();
            }

            _context.Entry(retired).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RetiredExists(id))
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

        // POST: api/Retireds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Retired>> PostRetired(Retired retired)
        {
          if (_context.Retireds == null)
          {
              return Problem("Entity set 'ComprehensiveDbContext.Retireds'  is null.");
          }
            _context.Retireds.Add(retired);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RetiredExists(retired.RetiredId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRetired", new { id = retired.RetiredId }, retired);
        }

        // DELETE: api/Retireds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRetired(long id)
        {
            if (_context.Retireds == null)
            {
                return NotFound();
            }
            var retired = await _context.Retireds.FindAsync(id);
            if (retired == null)
            {
                return NotFound();
            }

            _context.Retireds.Remove(retired);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RetiredExists(long id)
        {
            return (_context.Retireds?.Any(e => e.RetiredId == id)).GetValueOrDefault();
        }
    }
}
