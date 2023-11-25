using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comprehensive_backend.Models;
using Comprehensive_backend.Models.ViewModel;

namespace Comprehensive_backend.Controllers.RelatedControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatedsController : ControllerBase
    {
        private readonly ComprehensiveDbContext _context;

        public RelatedsController(ComprehensiveDbContext context)
        {
            _context = context;
        }

        // GET: api/Relateds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Related>>> GetRelateds()
        {
          if (_context.Relateds == null)
          {
              return NotFound();
          }
            return await _context.Relateds.ToListAsync();
        }

        // GET: api/Relateds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Related>> GetRelated(long id)
        {
          if (_context.Relateds == null)
          {
              return NotFound();
          }
            var related = await _context.Relateds.FindAsync(id);

            if (related == null)
            {
                return NotFound();
            }

            return related;
        }

        // PUT: api/Relateds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelated(long id, Related related)
        {
            if (id != related.RelatedId)
            {
                return BadRequest();
            }

            _context.Entry(related).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelatedExists(id))
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

        // POST: api/Relateds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Related>> PostRelated(RelatedDTO relateddto)
        {
          if (_context.Relateds == null)
          {
              return Problem("Entity set 'ComprehensiveDbContext.Relateds'  is null.");
          }

            var retired = await _context.Retireds.FindAsync(relateddto.RetiredId);
            var related = new Related();
            related.Retired = retired;
            related.RelatedId = relateddto.RelatedId;
            related.RelatedFirstName = relateddto.RelatedFirstName;
            related.RelatedLastName = relateddto.RelatedLastName;
            _context.Relateds.Add(related);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RelatedExists(related.RelatedId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRelated", new { id = related.RelatedId }, related);
        }

        // DELETE: api/Relateds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRelated(long id)
        {
            if (_context.Relateds == null)
            {
                return NotFound();
            }
            var related = await _context.Relateds.FindAsync(id);
            if (related == null)
            {
                return NotFound();
            }

            _context.Relateds.Remove(related);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RelatedExists(long id)
        {
            return (_context.Relateds?.Any(e => e.RelatedId == id)).GetValueOrDefault();
        }
    }
}
