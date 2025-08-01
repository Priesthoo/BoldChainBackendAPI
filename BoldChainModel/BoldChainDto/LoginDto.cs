﻿using System.ComponentModel.DataAnnotations;

namespace BoldChainBackendAPI.BoldChainModel.BoldChainDto
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email is required")]
        public string Email{ get; set; }
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
    }
}
