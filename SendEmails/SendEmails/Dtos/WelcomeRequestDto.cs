using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SendEmails.Dtos
{
    public class WelcomeRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
