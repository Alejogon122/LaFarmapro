using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace LaFarmapro.Models.viewmodels
{
        public class CLogin
        {
            [Required(ErrorMessage = "El usuario es requerido")]
            [Display(Name = "Usuario")]
            public string user { get; set; }

            [Required(ErrorMessage = "La contraseña es requerida")]
            [Display(Name = "Contraseña")]
            [DataType(DataType.Password)]
            public string password { get; set; }
        }
    
}