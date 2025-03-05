using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserAuth.Model
{
    public class User : IdentityUser
    {
        
        [Required]
        public string RegistrationNumber { get; set; } // Matricula do usuario
        [Required]
        public DateTime DateOfBirth { get; set; } // Data de nascimento
        [Required]
        public DateTime RegistronDate { get; set; } = DateTime.Now; // Data de registro

        // Construtor da classe "User"
        public User() : base() { }
    }
}
