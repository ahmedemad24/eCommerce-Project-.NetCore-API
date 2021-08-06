using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirbnbCRUD.Model;
using AirbnbCRUD.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;
using Microsoft.Extensions.Options;

namespace AirbnbCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPerson _person;
        private readonly ApplicationSettings _appSettings;
        private readonly ApplicationContext _db;

        public PeopleController(IPerson person, IOptions<ApplicationSettings> appSettings,ApplicationContext db )
        {
            _person = person;
            this._appSettings = appSettings.Value;
            this._db = db;

        }

        // GET: api/People
        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetPeople()
        {
            return _person.GetAllPeople();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public ActionResult<Person> GetPerson(int id)
        {
            var person = _person.GetPerson(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), DisableRequestSizeLimit]

        public IActionResult PutPerson(int id, [FromForm] Person person)
        {
            if (id != person.PersonId)
            {
                return BadRequest();
            }

            try
            {
                _person.EditPerson(person);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_person.PersonExists(id))
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

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost, DisableRequestSizeLimit]
        public ActionResult<Person> PostPerson([FromForm] Person person )
        {
            try
            {
   
                _person.CreatePerson(person);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",person.PersonId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                var ad = person.PersonId;
                return Ok(new { token });
            }
            catch(Exception e)
            {
                return Content($"{e.Message}");
            }

           
        }





        // DELETE: api/People/5

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            var person = _person.GetPerson(id);
            if (person == null)
            {
                return NotFound();
            }
            _person.DeletePerson(id);

            return NoContent();
        }


        [HttpPost("Login")]

        //POST : /api/Users/Login
        public async Task<IActionResult> Login(AppLoginModel model)
        {
            var user = await _db.People.FirstOrDefaultAsync(x => x.PersonEmailName == model.Email && x.PersonPassword == model.Password);
            if (user != null)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.PersonId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token, user.PersonId });
            }
            else
                return BadRequest();
        }

    }
}
