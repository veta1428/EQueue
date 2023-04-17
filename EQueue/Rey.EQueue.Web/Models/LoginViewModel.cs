﻿using System.ComponentModel.DataAnnotations;

namespace Rey.EQueue.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        public string? ReturnUrl { get; set; }
    }
}
