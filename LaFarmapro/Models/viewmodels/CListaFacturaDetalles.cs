using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaFarmapro.Models.viewModels
{
    public class CListaFacturaDetalles
    {

        public int idFacturaDetalle { get; set; }
        public int cantidad { get; set; }
        public decimal precioUnidad { get; set; }
        public decimal subtotal { get; set; }
        string codigoProducto { get; set; }
        string idFactura { get; set; }
    }
}