using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaFarmapro.Models.viewModels
{
    public class CListaUsuario
    {
        public int idUsuario { get; set; }
        public int identificacionUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string apellidoUsuario { get; set; }
        public string correoUsuario { get; set; }
        public string tipoUsuario { get; set; }
        public string estadoUsuario { get; set; }
    }
}