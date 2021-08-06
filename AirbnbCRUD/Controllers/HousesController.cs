using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirbnbCRUD.Model;
using AirbnbCRUD.Services;

namespace AirbnbCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousesController : ControllerBase
    {
        private readonly IHouse _house;
        public HousesController(IHouse house)
        {
            _house = house;
        }

        // GET: api/Houses
        [HttpGet]
        public ActionResult<IEnumerable<House>> GetHouses()
        {
            return _house.GetAllHouses().ToArray();
        }
        // GET: api/Houses/Alexandria
        [HttpGet("city/{city}")]
        public ActionResult<IEnumerable<House>> GetHousesInCity(string city)
        {
            var houses = _house.GetAllHousesInCity(city).ToArray();
            if (houses == null)
            {
                return NotFound();
            }
            return houses;
        }
        [HttpGet("person/{id}")]
        public ActionResult<IEnumerable<House>> GetHousesByPerson(int id)
        {
            var houses = _house.GetAllHousesByPersonId(id).ToArray();
            if (houses == null)
            {
                return NotFound();
            }
            return houses;
        }
        // GET: api/Houses/5
        [HttpGet("{id}")]
        public ActionResult<House> GetHouse(int id)
        {
            var house = _house.GetHouse(id);

            if (house == null)
            {
                return NotFound();
            }

            return house;
        }

        // PUT: api/Houses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult PutHouse(int id,  House house)
        {
            if (id != house.HouseId)
            {
                return BadRequest();
            }

            try
            {
                _house.EditHouse(house);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_house.HouseExists(id))
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

        // POST: api/Houses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<House> PostHouse([FromForm] House house)
        { 
            _house.CreateHouse(house);
            return Ok(house);
        }

        // DELETE: api/Houses/5
        [HttpDelete("{id}")]
        public IActionResult DeleteHouse(int id)
        {
            var house = GetHouse(id);
            if (house == null)
            {
                return NotFound();
            }

            _house.DeleteHouse(id);

            return NoContent();
        }
    }
}
