using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERPTwoTables.Models;

namespace SimpleERPTwoTables.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityLanguagesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CityLanguagesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/CityLanguages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityLanguage>>> GetCityLanguages()
        {
          if (_context.CityLanguages == null)
          {
              return NotFound();
          }
            return await _context.CityLanguages.ToListAsync();
        }

        // GET: api/CityLanguages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CityLanguage>> GetCityLanguage(int id)
        {
          if (_context.CityLanguages == null)
          {
              return NotFound();
          }
            var cityLanguage = await _context.CityLanguages.FindAsync(id);

            if (cityLanguage == null)
            {
                return NotFound();
            }

            return cityLanguage;
        }

        // PUT: api/CityLanguages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCityLanguage(int id, CityLanguage cityLanguage)
        {
            if (id != cityLanguage.CityId)
            {
                return BadRequest();
            }

            _context.Entry(cityLanguage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityLanguageExists(id))
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

        // POST: api/CityLanguages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CityLanguage>> PostCityLanguage(CityLanguage cityLanguage)
        {
          if (_context.CityLanguages == null)
          {
              return Problem("Entity set 'DatabaseContext.CityLanguages'  is null.");
          }
            _context.CityLanguages.Add(cityLanguage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CityLanguageExists(cityLanguage.CityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCityLanguage", new { id = cityLanguage.CityId }, cityLanguage);
        }

        // DELETE: api/CityLanguages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCityLanguage(int id)
        {
            if (_context.CityLanguages == null)
            {
                return NotFound();
            }
            var cityLanguage = await _context.CityLanguages.FindAsync(id);
            if (cityLanguage == null)
            {
                return NotFound();
            }

            _context.CityLanguages.Remove(cityLanguage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityLanguageExists(int id)
        {
            return (_context.CityLanguages?.Any(e => e.CityId == id)).GetValueOrDefault();
        }
    }
}
