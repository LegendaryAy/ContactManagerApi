using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class User : IdentityUser
    {
        [StringLength(40)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(40)]
        [Required]
        public string LastName { get; set; }

    }
}
