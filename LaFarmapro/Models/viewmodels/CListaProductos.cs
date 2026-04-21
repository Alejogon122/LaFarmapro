using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaFarmapro.Models.viewModels
{
    public class CListaProductos
    {
     public int idProducto { get; set; }
     public string nombreProducto { get; set; }
     public string descripcionProducto { get; set; }
     public decimal precioProducto { get; set; }
     public string estadoProducto { get; set; }
    }
}