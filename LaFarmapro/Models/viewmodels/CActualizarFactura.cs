using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaFarmapro.Models.viewModels
{
    public class CActualizarFactura
    {
        public int idFactura { get; set; }
        [Required]
        [Display(Name = "Fecha de Facturacion")]
        public DateTime fecha { get; set; }
        [Required]
        [Display(Name = "Metodo de Pago")]
        public string metodoPago { get; set; }
        [Required]
        [Display(Name = "Total")]
        [Range(0, 9999999)]
        public decimal total { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public int idCliente { get; set; }

        public IEnumerable<SelectListItem> Clientes { get; set; }
        public IEnumerable<SelectListItem> Productos { get; set; }

    }
}