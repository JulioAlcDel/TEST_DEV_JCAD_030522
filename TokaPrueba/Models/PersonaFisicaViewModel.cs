using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;


namespace TokaPrueba.Models
{
    public class PersonaFisicaViewModel
    {
       
        public int IdPersonaFisica { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Ingrese solo letras")]
        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; }
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Ingrese solo letras")]
        [Display(Name = "Apellido Materno")]
        public string ApellidoMaterno { get; set; }
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [RegularExpression(@"^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$", ErrorMessage = "Rfc no valido")]
        [StringLength(14, ErrorMessage = "Solo se permiten 13 caracteres")]
        public string RFC { get; set; }
        [DataType(DataType.Date)]
        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [Display(Name = "Fecha Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        public int UsuarioAgrega { get; set; }
        public bool Activo { get; set; } = true;
    }
}
