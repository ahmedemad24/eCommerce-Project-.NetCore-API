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
    public class House
    {
        [Key]

        public int HouseId { get; set; }

        [Required]
        public string HouseCity { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string HouseCountry { get; set; }

        [Required]
        public string HouseStreet { get; set; }
        [Required]
        public string HouseDescription { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        [Required]
        public int HousePrice { get; set; }

        [NotMapped]
        public IFormFile[] HousePhotoFiles { get; set; }
        public string HousePhotoName { get; set; }

        [ForeignKey("Person")]
        [Required]
        public int PersonId { get; set; }

        [JsonIgnore]
        public virtual Person Person { get; set; }
        [JsonIgnore]
        public virtual ICollection<HousePhoto> HousePhotos { get; set; } = new HashSet<HousePhoto>();
        [JsonIgnore]
        public virtual ICollection<HouseFeedback> Feedbacks { get; set; } = new HashSet<HouseFeedback>();
        [JsonIgnore]
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}
