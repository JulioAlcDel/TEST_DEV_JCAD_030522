using System.ComponentModel.DataAnnotations;

namespace TokaPrueba.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [EmailAddress(ErrorMessage = "El campo debe de ser un correo Valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
