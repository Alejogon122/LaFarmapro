using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaFarmapro.Models.viewModels
{
    public class CActualizarProducto
    {
        public int idProducto { get; set; }

        [Required]
        [Display(Name = "Nombre del Producto")]
        public string nombreProducto { get; set; }

        [Required]
        [Display(Name = "Descripcion del Producto")]
        public string descripcionProducto { get; set; }

        [Required]
        [Display(Name = "Precio del Producto")]
        public decimal precioProducto { get; set; }

        [Required]
        [Display(Name = "Estado del Producto")]
        public string estadoProducto { get; set; }
    }
}