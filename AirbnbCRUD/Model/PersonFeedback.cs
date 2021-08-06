using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AirbnbCRUD.Model
{
    public class PersonFeedback
    {
        [Key]
        [Display(Name = "Feedback ID")]
        public int PersonFeedbackId { get; set; }

        [Display(Name = "Content")]
        public string PersonFeedbackBody { get; set; }

        [Range(1,5)]
        [Display(Name = "Stars")]
        public int PersonFeedbackStars { get; set; }

        [Display(Name = "Date")]
        public DateTime PersonFeedbackDate { get; set; }
        [ForeignKey("PersonAsHost")]
        public int PersonHostId { get; set; }
        [ForeignKey("PersonAsCustomer")]
        public int PersonCustomerId { get; set; }
        [JsonIgnore]

        public virtual Person PersonAsHost { get; set; }
        [JsonIgnore]

        public virtual Person PersonAsCustomer { get; set; }

    }
}
