using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERPTwoTables.DTO;
using SimpleERPTwoTables.Models;

namespace SimpleERPTwoTables.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public LanguagesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Languages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageDTO>>> GetLanguages()
        {
            List<Language> LanguageList = new List<Language>();

            if (_context.Languages == null)
            {
                return NotFound();
            }

            LanguageList = await _context.Languages.Include(c => c.CityLanguages).ThenInclude(c => c.City).ToListAsync();
            //return await _context.Languages.ToListAsync();

            List<LanguageDTO> LanguageDTOList = new List<LanguageDTO>();  

            LanguageDTOList = LanguageList.Adapt<LanguageDTO[]>().ToList();

            return Ok(LanguageDTOList);
        }

        // GET: api/Languages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Language>> GetLanguage(int id)
        {
          if (_context.Languages == null)
          {
              return NotFound();
          }
            var language = await _context.Languages.FindAsync(id);

            if (language == null)
            {
                return NotFound();
            }

            return language;
        }

        // PUT: api/Languages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguage(int id, Language language)
        {
            if (id != language.LanguageId)
            {
                return BadRequest();
            }

            _context.Entry(language).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageExists(id))
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

        // POST: api/Languages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Language>> PostLanguage(Language language)
        {
          if (_context.Languages == null)
          {
              return Problem("Entity set 'DatabaseContext.Languages'  is null.");
          }
            _context.Languages.Add(language);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLanguage", new { id = language.LanguageId }, language);
        }

        // DELETE: api/Languages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            if (_context.Languages == null)
            {
                return NotFound();
            }
            var language = await _context.Languages.FindAsync(id);
            if (language == null)
            {
                return NotFound();
            }

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LanguageExists(int id)
        {
            return (_context.Languages?.Any(e => e.LanguageId == id)).GetValueOrDefault();
        }
    }
}
