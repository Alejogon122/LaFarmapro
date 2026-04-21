using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LaFarmapro.Models.viewModels
{
    public class CAgregarCliente
    {
        [Required]
        [Display(Name = "Identificacion")]
        public int identificacionCliente { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string nombreCliente { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string apellidoCliente { get; set; }

        [Required]
        [Display(Name = "Correo")]
        [EmailAddress]
        public string correoCliente { get; set; }

    }
}