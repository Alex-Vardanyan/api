using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Api.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$", 
            ErrorMessage = "Your password must be at least 8 characters long and contain at least 1 number and 1 uppercase and 1 lowercase")]
        public string Password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phonenumber { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string street { get; set; }
        public int buildingnumber { get; set; }
        public string apartment { get; set; }
    }
}
