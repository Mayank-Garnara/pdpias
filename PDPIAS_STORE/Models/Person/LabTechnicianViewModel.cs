using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PDPIAS_management.Models.Person
{
    public class LabTechnicianViewModel
    {
        public int Id {  get; set; }

        [Required(ErrorMessage = "First name is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "First name must contain only letters.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Firstname must be 1 to 50 character long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Last name must contain only letters.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Lastname must be 1 to 50 character long")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*[0-9])(?=.*[!@#$%^&*(),.?\":{}|<>_\\-+=~`[\\]\\\\;/])(?=.*[A-Z])(?=.*[a-z]).{8,15}$", ErrorMessage = "Password must be 8–15 chars with upper, lower, digit & special char.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*[0-9])(?=.*[!@#$%^&*(),.?\":{}|<>_\\-+=~`[\\]\\\\;/])(?=.*[A-Z])(?=.*[a-z]).{8,15}$", ErrorMessage = "Password must be 8–15 chars with upper, lower, digit & special char.")]
        public string ConfirmPassword { get; set; }

        public int department_id { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"^(\+91[\-\s]?)?[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit Indian mobile number.")]
        public string ContactNo { get; set; }


        [Required(ErrorMessage = "Please upload profile picture")]
        public HttpPostedFileBase ProfilePic { get; set; }
    }
}