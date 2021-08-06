using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AirbnbCRUD.Model
{
    public class Person
    {
        [Key]
        [Display(Name = "ID")]
        public int PersonId { get; set; }
        [Required]
        public string PersonFirstName { get; set; }
        [Required]

        public string PersonLastName { get; set; }
        [Required]

        public string PersonEmailName { get; set; }
        [Required]

        public string PersonPhone { get; set; }

        [NotMapped]
        public IFormFile ProfilePicture { get; set; }


        public string ProfilePictureName { get; set; }
        [Required]
        public string PersonPassword { get; set; }
        [JsonIgnore]
        public virtual ICollection<House> Houses { get; set; } = new HashSet<House>();
        [JsonIgnore]
        public virtual ICollection<PersonFeedback> FeedbacksAsHost { get; set; } = new HashSet<PersonFeedback>();
        [JsonIgnore]
        public virtual ICollection<PersonFeedback> FeedbacksAsCustomer { get; set; } = new HashSet<PersonFeedback>();
        [JsonIgnore]
        public virtual ICollection<HouseFeedback> FeedbacksOnHouses { get; set; } = new HashSet<HouseFeedback>();
        [JsonIgnore]
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();


    }
}
