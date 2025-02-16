﻿using System.ComponentModel.DataAnnotations;

namespace WarehouseAPI.Domain.Models.User
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "CompanyId is required")]
        public required int CompanyId { get; set; }
    }
}
