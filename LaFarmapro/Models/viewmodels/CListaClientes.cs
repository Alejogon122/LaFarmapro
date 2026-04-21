using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaFarmapro.Models.viewModels
{
    public class CListaClientes
    {
        public int idCliente { get; set; }
        public int identificacionCliente { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoCliente { get; set; }
        public string correoCliente { get; set; }
    }
}