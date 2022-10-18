using System;
using System.ComponentModel.DataAnnotations;

namespace RookiesMVC.Models
{
    public class PersonCreateModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public string? BirthPlace { get; set; }
    }
}