using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medebr.Data.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public String UserName { get; set; }

        [Required]
        [StringLength(
            100,
            ErrorMessage = "The {0} must be at least {2} characters long.",
            MinimumLength = 7)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public String Password { get; set; }

        [Required]
        [Display(Name = "Email")]
        public String Email { get; set; }

        public String ReturnUrl { get; set; }
    }
}
