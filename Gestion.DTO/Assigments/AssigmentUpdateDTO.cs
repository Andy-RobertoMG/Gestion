using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion.DTO.Assigments
{
    public class AssigmentUpdateDTO
    {
        [MaxLength(80, ErrorMessage = "Se tiene permitido hasta 80 caracteres")]
        [Required(ErrorMessage = "El campo es requerido")] 
        public string AssignmentName { get; set; } = string.Empty;
    }
}
