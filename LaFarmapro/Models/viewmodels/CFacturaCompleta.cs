using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LaFarmapro.Models.viewModels
{

    public class CDetalleFacturaItem
    {
        public int idDetalle { get; set; }

        [Required]
        [Display(Name = "Producto")]
        public int idProducto { get; set; }

        public string nombreProducto { get; set; }

        [Required]
        [Range(1, 9999)]
        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }

        [Required]
        [Range(0.01, 9999999)]
        [Display(Name = "Precio Unidad")]
        public decimal precioUnidad { get; set; }

        public decimal subtotal { get; set; }


    }


    public class CFacturaCompleta
    {
    
        public int idFactura { get; set; }

        [Required]
        [Display(Name = "Fecha de Facturación")]
        public DateTime fecha { get; set; }

        [Required]
        [Display(Name = "Método de Pago")]
        public string metodoPago { get; set; }

        [Display(Name = "Total")]
        public decimal total { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public int idCliente { get; set; }

        public string nombreCliente { get; set; }

      
        public List<CDetalleFacturaItem> detalles { get; set; } = new List<CDetalleFacturaItem>();

 
       
        public IEnumerable<SelectListItem> Clientes { get; set; }
        public IEnumerable<SelectListItem> Productos { get; set; }
    }
}