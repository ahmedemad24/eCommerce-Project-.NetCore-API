using AirbnbCRUD.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataUserController : ControllerBase
    {
        private readonly IPerson _person;


        public DataUserController(IPerson person)
        {
            _person = person;
            

        }

        [HttpGet]
        [Route("profile")]
        //GET : /api/UserProfile
        public  object GetUserProfileId()
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;

            var ss = int.Parse(userId);
            var user =  _person.GetPerson(ss);
            var data = new
            {
                user.PersonId,
                user.PersonFirstName
            };



            return data;
        }
    }
}
