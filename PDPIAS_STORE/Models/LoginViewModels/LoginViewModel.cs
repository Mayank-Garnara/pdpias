using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PDPIAS_management.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Enter valid email address")]
        public String email { get; set; }


        [Required(ErrorMessage ="Password is required")]
        public String password { get; set; }
    }
}