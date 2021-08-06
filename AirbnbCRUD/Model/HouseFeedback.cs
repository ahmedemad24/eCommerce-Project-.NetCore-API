using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AirbnbCRUD.Model
{
    public class HouseFeedback
    {
        [Key]
        [Display(Name = "Feedback ID")]
        public int HouseFeedbackId { get; set; }

        [Display(Name = "Content")]
        public string HouseFeedbackBody { get; set; }

        [Range(1,5)]
        [Display(Name = "Stars")]
        public int HouseFeedbackStars { get; set; }

        [Display(Name = "Date")]
        public DateTime HouseFeedbackDate { get; set; }
        [ForeignKey("House")]
        public int HouseId { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        [JsonIgnore]

        public virtual House House { get; set; }
        [JsonIgnore]

        public virtual Person Person { get; set; }

    }
}
