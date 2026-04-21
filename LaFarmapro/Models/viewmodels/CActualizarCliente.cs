using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaFarmapro.Models.viewModels
{
    public class CActualizarCliente
    {
        public int idCliente { get; set; }

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