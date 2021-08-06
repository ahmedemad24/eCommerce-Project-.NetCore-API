using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AirbnbCRUD.Model
{
    public class HousePhoto
    {
        [ForeignKey("House")]
        [Display(Name = "House ID")]
        public int HouseId { get; set; }

        [Display(Name = "House Photo")]
        public  string HousePhotos { get; set; }

        [JsonIgnore]
        public virtual House House { get; set; }
    }
}
