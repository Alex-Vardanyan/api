using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Api.Dtos
{
    public class RegisterMemberDto
    {
        [Required(ErrorMessage = "Username is Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$", 
            ErrorMessage = "Your password must be at least 8 characters long and contain at least 1 number and 1 uppercase and 1 lowercase")]
        public string Password { get; set; }
        [Required(ErrorMessage = "First name is Required")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Last name is Required")]
        public string Lastname { get; set; }
        public string Phonenumber { get; set; }
        [Required(ErrorMessage = "Location info is Required")]
        public LocationDto Location { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public int StartingSalary { get; set; }
    }
}
