using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERPTwoTables.DTO;
using SimpleERPTwoTables.Models;

using Mapster;

namespace SimpleERPTwoTables.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CitiesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCities()
        {
            List<City> CityList = new List<City>();

            if (_context.Cities == null)
            {
                return NotFound();
            }
            //return await _context.Cities.ToListAsync();
            //return await _context.Cities.Include(c => c.Country).ToListAsync();

            //CityList = await _context.Cities.ToListAsync(); 
            CityList = await _context.Cities.
                Include(c => c.Country).
                Include(c => c.CityLanguages).
                ThenInclude(l => l.Language).ToListAsync();

            List<CityDTO> CityDTOList = new List<CityDTO>();
            
            //CityDTOList = CityList.Adapt(CityDTOList);
            CityDTOList = CityList.Adapt<CityDTO[]>().ToList();

            return Ok(CityDTOList);
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDTO>> GetCity(int id)
        {
            if (_context.Cities == null)
            {
                return NotFound();
            }
            var city = await _context.Cities.
                Include(c => c.Country).
                Include(c => c.CityLanguages).
                ThenInclude(l => l.Language).   
                FirstOrDefaultAsync(c => c.CityId == id);

            if (city == null)
            {
                return NotFound();
            }

            CityDTO CityDTOObject = city.Adapt<CityDTO>();

            return Ok(CityDTOObject);
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, CityForUpdateDTO cityDTO)
        {
            if (id != cityDTO.CityId)
            {
                return BadRequest();
            }

            var city = cityDTO.Adapt<City>();   

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CityForSaveDTO>> PostCity(CityForSaveDTO city)
        {
            if (_context.Cities == null)
            {
                return Problem("Entity set 'DatabaseContext.Cities'  is null.");
            }

            City CityObject = new City();
            CityObject = city.Adapt<City>();

            _context.Cities.Add(CityObject);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = CityObject.CityId }, city);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            if (_context.Cities == null)
            {
                return NotFound();
            }
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return (_context.Cities?.Any(e => e.CityId == id)).GetValueOrDefault();
        }

        private static CityDTO ItemToCity(City city, bool IncludeRelations = true)
        {
            CityDTO CityDTO_Object = new CityDTO();

            CityDTO_Object.CityId = city.CityId;
            CityDTO_Object.CityName = city.CityName;
            CityDTO_Object.CityDescription = city.CityDescription;
            CityDTO_Object.CountryId = city.CountryId;

            if (IncludeRelations)
            {
                CityDTO_Object.Country = new CountryDTOMinusRelations();

                CityDTO_Object.Country.CountryName = city.Country.CountryName;
                CityDTO_Object.Country.CountryId = city.CountryId;
            }

            return CityDTO_Object;

        }
    }
}
