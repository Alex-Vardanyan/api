using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Api.Dtos
{
    public class LocationDto
    {
        [Required(ErrorMessage = "City is Required")]
        public string city { get; set; }
        [Required(ErrorMessage = "District is Required")]
        public string district { get; set; }
        [Required(ErrorMessage = "Street is Required")]
        public string street { get; set; }
        [Required(ErrorMessage = "Building Number is Required")]
        public int buildingnumber { get; set; }
        [Required(ErrorMessage = "Apartment number is Required")]
        public string apartment { get; set; }
        
        public int? PostalCode { get; set; }
    }
}
