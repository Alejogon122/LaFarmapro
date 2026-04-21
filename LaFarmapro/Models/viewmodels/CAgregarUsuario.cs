using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LaFarmapro.Models.viewModels
{
    public class CAgregarUsuario

    {
        [Required]
        [Display(Name = "Identificacion del Usuario")]
        public int identificacionUsuario { get; set; }

        [Required]
        [Display(Name = "Nombre del Usuario")]
        public string nombreUsuario { get; set; }

        [Required]
        [Display(Name = "Apellido del Usuario")]
        public string apellidoUsuario { get; set; }

        [Required]
        [Display(Name = "Correo del Usuario")]
        [EmailAddress]
        public string correoUsuario { get; set; }

        [Required]
        [Display(Name = "Tipo de usuario")]
        public string tipoUsuario { get; set; }

        [Required]
        [Display(Name = "Estado del Usuario")]
        public string estadoUsuario { get; set; }

    }
}