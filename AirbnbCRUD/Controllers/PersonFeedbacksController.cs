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
    public class PersonFeedbacksController : ControllerBase
    {
        private readonly IPersonFeedback _personFeedback;

        public PersonFeedbacksController(IPersonFeedback personFeedback)
        {
            _personFeedback = personFeedback;
        }

        // GET: api/PersonFeedbacks
        [HttpGet]
        public ActionResult<IEnumerable<PersonFeedback>> GetPersonFeedbacks()
        {
            return _personFeedback.GetAllPeopleFeedbacks();
        }

        // GET: api/PersonFeedbacks/5
        [HttpGet("{id}")]
        public ActionResult<PersonFeedback> GetPersonFeedback(int id)
        {
            var personFeedback = _personFeedback.GetPersonFeedback(id);

            if (personFeedback == null)
            {
                return NotFound();
            }

            return personFeedback;
        }

        [HttpGet("Host/{id}")]
        public ActionResult<List<PersonFeedback>> GetPersonHostFeedback(int id)
        {
            var personFeedback = _personFeedback.GetAllPeopleFeedbacksByHostId(id);

            if (personFeedback == null)
            {
                return NotFound();
            }

            return personFeedback;
        }

        [HttpGet("Customer/{id}")]
        public ActionResult<List<PersonFeedback>> GetPersonCustomerFeedback(int id)
        {
            var personFeedback = _personFeedback.GetAllPeopleFeedbacksByCustomerId(id);

            if (personFeedback == null)
            {
                return NotFound();
            }

            return personFeedback;
        }

        // PUT: api/PersonFeedbacks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutPersonFeedback(int id, PersonFeedback personFeedback)
        {
            if (id != personFeedback.PersonFeedbackId)
            {
                return BadRequest();
            }

            try
            {
                _personFeedback.EditPersonFeedback(personFeedback);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_personFeedback.PersonFeedbackExists(id))
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

        // POST: api/PersonFeedbacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<PersonFeedback> PostPersonFeedback(PersonFeedback personFeedback)
        {
            _personFeedback.CreatePersonFeedback(personFeedback);

            return CreatedAtAction("GetPersonFeedback", new { id = personFeedback.PersonFeedbackId }, personFeedback);
        }

        // DELETE: api/PersonFeedbacks/5
        [HttpDelete("{id}")]
        public IActionResult DeletePersonFeedback(int id)
        {
            var personFeedback = _personFeedback.GetPersonFeedback(id);
            if (personFeedback == null)
            {
                return NotFound();
            }

            _personFeedback.DeletePersonFeedback(id);

            return NoContent();
        }
    }
}
