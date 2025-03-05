using System.ComponentModel.DataAnnotations;

namespace UserAuth.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required]
        public string UserName { get; set; } // Nome do usuario
        [Required]
        public string RegistrationNumber { get; set; } // Matricula do usuario
        [Required]
        public DateTime DateOfBirth { get; set; } // Data de nascimento
        [Required]
        [DataType(DataType.Password)] // Tipo de dado "senha"
        public string Password { get; set; } // Senha
        [Required]
        [Compare("Password")] // Comparação do Password
        public string PasswordConfirmation { get; set; } // Confirmação da senha
        [Required]
        public DateTime RegistronDate { get; set; } = DateTime.Now; // Data de registro
    }
}
