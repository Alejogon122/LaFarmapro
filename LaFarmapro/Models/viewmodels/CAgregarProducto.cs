using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LaFarmapro.Models.viewModels
{
    public class CAgregarProducto
    {
    

    [Required]
    [Display(Name = "Nombre del Producto")]
    public string nombreProducto { get; set; }

    [Required]
    [Display(Name = "Descripcion del Producto")]
    public string descripcionProducto { get;set; }

    [Required]
    [Display(Name = "Precio del Producto")]
    public decimal precioProducto { get; set; }

    [Required]
    [Display(Name = "Estado del Producto")]
    public string estadoProducto { get; set; }


   
    }
}