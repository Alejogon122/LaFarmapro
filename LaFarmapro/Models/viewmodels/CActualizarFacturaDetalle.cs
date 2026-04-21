using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaFarmapro.Models.viewModels
{
    public class CActualizarFacturaDetalle
    {
        public int idFacturaDetalle { get; set; }

        [Required]
        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }

        [Required]
        [Display(Name = "Precio por Unidad")]
        public decimal precioUnidad { get; set; }

        [Required]
        [Display(Name = "Subtotal")]
        public decimal subtotal { get; set; }

        [Required]
        [Display(Name = "Codigo del Producto")]
        public string idProducto { get; set; }

        [Required]
        [Display(Name = "Codigo de Factura")]
        public string idFactura { get; set; }


        public IEnumerable<SelectListItem> Facturas { get; set; }
        public IEnumerable<SelectListItem> Productos { get; set; }
    }
}