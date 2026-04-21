using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaFarmapro.Models.viewModels
{
    public class CListaFacturas
    {
        public int idFactura { get; set; }
        public DateTime fecha { get; set; }
        public string metodoPago { get; set; }
        public decimal total { get; set; }
        public int idCliente { get; set; }
    }
}