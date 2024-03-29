﻿using System.ComponentModel.DataAnnotations;

namespace WepAppMvc.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
