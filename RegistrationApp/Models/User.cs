﻿using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
