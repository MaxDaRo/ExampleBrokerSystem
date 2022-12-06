﻿using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Data.ViewModels.Auth
{
    public class AuthenticationRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
