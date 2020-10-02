using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace User.Shared.Models.Input
{
    public class CreateNewUserInputModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(45,MinimumLength =5)]
        public string UserName { get; set; }

        [Required]
        [StringLength(45,MinimumLength =4)]
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
