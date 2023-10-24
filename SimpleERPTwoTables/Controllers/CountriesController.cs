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
    public class CountriesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CountriesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountries()
        {
            List<Country> CountryList = new List<Country>();    

            if (_context.Countries == null)
            {
                return NotFound();
            }
            //return await _context.Countries.Include(c => c.Cities).ToListAsync();

            CountryList = await _context.Countries.Include(c => c.Cities).ToListAsync();
            
            List<CountryDTO> CountriesDTOList = new List<CountryDTO>();

            CountriesDTOList = CountryList.Adapt<CountryDTO[]>().ToList();

            return Ok(CountriesDTOList);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDTO>> GetCountry(int id)
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            var country = await _context.Countries.Include(c => c.Cities).FirstOrDefaultAsync(c => c.CountryId == id);

            if (country == null)
            {
                return NotFound();
            }

            CountryDTO CountryDTOObject = country.Adapt<CountryDTO>();

            return Ok(CountryDTOObject);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, CountryForUpdateDTO countryDTO)
        {
            if (id != countryDTO.CountryId)
            {
                return BadRequest();
            }

            var country = countryDTO.Adapt<Country>();  

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CountryForSaveDTO>> PostCountry(CountryForSaveDTO country)
        {
            if (_context.Countries == null)
            {
                return Problem("Entity set 'DatabaseContext.Countries'  is null.");
            }

            Country CountryObjekt = new Country();
            CountryObjekt = country.Adapt<Country>();

            _context.Countries.Add(CountryObjekt);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = CountryObjekt.CountryId }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return (_context.Countries?.Any(e => e.CountryId == id)).GetValueOrDefault();
        }
    }
}
