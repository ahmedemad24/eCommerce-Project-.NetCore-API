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
    public class HouseFeedbacksController : ControllerBase
    {
        private readonly IHouseFeedback _houseFeedback;

        public HouseFeedbacksController(ApplicationContext context,IHouseFeedback houseFeedback)
        {
            _houseFeedback = houseFeedback;
        }

        // GET: api/HouseFeedbacks
        [HttpGet]
        public ActionResult<IEnumerable<HouseFeedback>> GetHouseFeedbacks()
        {
            return _houseFeedback.GetAllHousesFeedbacks();
        }

        [HttpGet("HouseFeedbacks/{id}")]
        public ActionResult<IEnumerable<HouseFeedback>> GetHouseFeedbacks(int id)
        {
            var feedbacks = _houseFeedback.GetHouseFeedbacks(id);
            if (feedbacks == null)
            {
                return NotFound();
            }

            return feedbacks;
        }

        // GET: api/HouseFeedbacks/5
        [HttpGet("{id}")]
        public ActionResult<HouseFeedback> GetHouseFeedback(int id)
        {
            var houseFeedback = _houseFeedback.GetHouseFeedback(id);

            if (houseFeedback == null)
            {
                return NotFound();
            }

            return houseFeedback;
        }

        // PUT: api/HouseFeedbacks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutHouseFeedback(int id, HouseFeedback houseFeedback)
        {
            if (id != houseFeedback.HouseFeedbackId)
            {
                return BadRequest();
            }


            try
            {
                _houseFeedback.EditHouseFeedback(houseFeedback);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_houseFeedback.HouseFeedbackExists(id))
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

        // POST: api/HouseFeedbacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<HouseFeedback> PostHouseFeedback(HouseFeedback houseFeedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _houseFeedback.CreateHouseFeedback(houseFeedback);

            return CreatedAtAction("GetHouseFeedback", new { id = houseFeedback.HouseFeedbackId }, houseFeedback);
        }

        // DELETE: api/HouseFeedbacks/5
        [HttpDelete("{id}")]
        public IActionResult DeleteHouseFeedback(int id)
        {
            var houseFeedback = _houseFeedback.GetHouseFeedback(id);
            if (houseFeedback == null)
            {
                return NotFound();
            }

            _houseFeedback.DeleteHouseFeedback(id);

            return NoContent();
        }
    }
}
