using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AirbnbCRUD.Model
{
    public class Booking
    {
        [Key]
        [Display(Name = "Booking ID")]
        public int BookingId { get; set; }
        [ForeignKey("Person")]
        [Required]
        public int PersonId { get; set; }
        [ForeignKey("House")]
        [Required]
        public int HouseId { get; set; }
        [Required]
        [Display(Name = "StartDate")]
        public DateTime StartBookingDate { get; set; }
        [Required]

        [Display(Name = "EndDate")]
        public DateTime EndBookingDate { get; set; }
        [Required]

        [Display(Name = "CreatedDate")]
        public DateTime CreatingBookingDate { get; set; }
        [JsonIgnore]

        public virtual Person Person { get; set; }
        [JsonIgnore]

        public virtual House House { get; set; }
    }
}
