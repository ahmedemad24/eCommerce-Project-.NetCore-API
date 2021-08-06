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
    public class HousePhotoesController : ControllerBase
    {
        private readonly IHousePhoto _housePhoto;

        public HousePhotoesController(IHousePhoto housePhoto)
        {
            _housePhoto = housePhoto;
        }

        // GET: api/HousePhotoes
        [HttpGet]
        public ActionResult<IEnumerable<HousePhoto>> GetHousePhotos()
        {
            return _housePhoto.GetAllHousePhotos().ToArray();
        }

        // GET: api/HousePhotoes/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<HousePhoto>> GetHousePhotoes(int id)
        {
            var housePhoto = _housePhoto.GetHousePhotos(id);

            if (housePhoto == null)
            {
                return NotFound();
            }

            return housePhoto.ToArray();
        }

        // GET: api/HousePhotoes/5
        //[HttpGet("{id}")]
        //public ActionResult<HousePhoto> GetHousePhoto(int id,string housePhotoNumber)
        //{
        //    var housePhoto = _housePhoto.GetHousePhoto(id,housePhotoNumber);

        //    if (housePhoto == null)
        //    {
        //        return NotFound();
        //    }

        //    return housePhoto;
        //}

        // PUT: api/HousePhotoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutHousePhoto(int id,string housePhotoNumber, HousePhoto housePhoto)
        {
            if (id != housePhoto.HouseId && housePhotoNumber!=housePhoto.HousePhotos)
            {
                return BadRequest();
            }

            try
            {
                _housePhoto.EditHousephoto(housePhoto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_housePhoto.HousePhotoExists(id,housePhotoNumber))
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

        // POST: api/HousePhotoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<HousePhoto> PostHousePhoto(HousePhoto housePhoto)
        {
            try
            {
                _housePhoto.CreateHousePhoto(housePhoto);
                return Ok(housePhoto);
            }
            catch (DbUpdateException)
            {
                if (_housePhoto.HousePhotoExists(housePhoto.HouseId,housePhoto.HousePhotos))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // DELETE: api/HousePhotoes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteHousePhoto(int id,string housePhotoNumber)
        {
            var housePhoto = _housePhoto.GetHousePhoto(housePhotoNumber);
            if (housePhoto == null)
            {
                return NotFound();
            }

            _housePhoto.DeleteHousePhoto(housePhotoNumber);

            return NoContent();
        }
    }
}
