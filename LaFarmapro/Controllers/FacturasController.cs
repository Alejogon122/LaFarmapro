
using LaFarmapro;
using LaFarmapro.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaFarma.Controllers
{
    public class FacturasController : Controller
    {
        // GET: Facturas
        public ActionResult mantFacturas()
        {
            List<CListaFacturas> lista = null;
            using (var db = new LaFarmaciaEntities())
            {
                lista = (from f in db.FACTURAS
                         select new CListaFacturas
                         {
                             idFactura = f.ID_FACTURA,
                             fecha = f.FECHA,
                             metodoPago = f.METODO_PAGO,
                             total = f.TOTAL,
                             idCliente = f.ID_CLIENTE
                         }).ToList();
            }
            return View(lista);
        }

        // GET: Facturas/Details/5
        public ActionResult consultarFactura(int id)
        {
            CListaFacturas model = new CListaFacturas();
            using (var db = new LaFarmaciaEntities())
            {
                var factura = db.FACTURAS.Find(id);
                if (factura == null) return HttpNotFound();
                model.idFactura = factura.ID_FACTURA;
                model.fecha = factura.FECHA;
                model.metodoPago = factura.METODO_PAGO;
                model.total = factura.TOTAL;
                model.idCliente = factura.ID_CLIENTE;
            }
            return View(model);
        }

        // ============================================================
        //  GESTIÓN UNIFICADA: Factura + Detalles en una sola pantalla
        // ============================================================

        // GET: Facturas/gestionarFactura (nueva)
        [HttpGet]
        public ActionResult gestionarFactura()
        {
            var model = new CFacturaCompleta();
            model.fecha = DateTime.Today;
            model.detalles = new List<CDetalleFacturaItem>
    {
        new CDetalleFacturaItem()
    };
            CargarListas(model);
            return View(model);
        }

        // GET: Facturas/gestionarFactura/5 (editar existente)
        [HttpGet]
        public ActionResult editarFacturaCompleta(int id)
        {
            var model = new CFacturaCompleta();
            using (var db = new LaFarmaciaEntities())
            {
                var factura = db.FACTURAS.Find(id);
                if (factura == null) return HttpNotFound();

                model.idFactura = factura.ID_FACTURA;
                model.fecha = factura.FECHA;
                model.metodoPago = factura.METODO_PAGO;
                model.total = factura.TOTAL;
                model.idCliente = factura.ID_CLIENTE;

                // Cargar detalles existentes

                model.detalles = (from d in db.DETALLE_FACTURA
                                  where d.ID_FACTURA == id
                                  join p in db.PRODUCTO on d.ID_PRODUCTO equals p.ID_PRODUCTO into pj
                                  from p in pj.DefaultIfEmpty()
                                  select new CDetalleFacturaItem
                                  {
                                      idDetalle = d.ID_DETALLE_FACTURA, 
                                      idProducto = d.ID_PRODUCTO,
                                      nombreProducto = p != null ? p.NOMBRE : d.ID_PRODUCTO.ToString(),
                                      cantidad = d.CANTIDAD,
                                      precioUnidad = d.PRECIO_UNIDAD,
                                      subtotal = d.SUBTOTAL ?? 0
                                  }).ToList();

                CargarListas(model, db);
            }
            return View("gestionarFactura", model);
        }

        // POST: guardar factura + detalles
        [HttpPost]
        public ActionResult guardarFacturaCompleta(CFacturaCompleta model,
     int[] idProducto, int[] cantidad, decimal[] precioUnidad)
        {
            try
            {
                idProducto = idProducto ?? new int[0];
                cantidad = cantidad ?? new int[0];
                precioUnidad = precioUnidad ?? new decimal[0];

                using (var db = new LaFarmaciaEntities())
                {
                    FACTURAS factura;

                    if (model.idFactura == 0)
                    {
                        factura = new FACTURAS
                        {
                            FECHA = model.fecha,
                            METODO_PAGO = model.metodoPago,
                            TOTAL = 0,
                            ID_CLIENTE = model.idCliente
                        };
                        db.FACTURAS.Add(factura);
                        db.SaveChanges();
                    }
                    else
                    {
                        factura = db.FACTURAS.Find(model.idFactura);
                        if (factura == null) return HttpNotFound();
                        factura.FECHA = model.fecha;
                        factura.METODO_PAGO = model.metodoPago;
                        factura.ID_CLIENTE = model.idCliente;

                        var detallesViejos = db.DETALLE_FACTURA
                            .Where(d => d.ID_FACTURA == factura.ID_FACTURA).ToList();
                        db.DETALLE_FACTURA.RemoveRange(detallesViejos);
                        db.SaveChanges();
                    }

                    // Insertar detalles y calcular total
                    decimal totalFactura = 0;
                    for (int i = 0; i < idProducto.Length; i++)
                    {
                        if (idProducto[i] != 0 && i < cantidad.Length && i < precioUnidad.Length && cantidad[i] > 0 && precioUnidad[i] > 0)
                        {
                            decimal subtotal = cantidad[i] * precioUnidad[i];
                            totalFactura += subtotal;

                            db.DETALLE_FACTURA.Add(new DETALLE_FACTURA
                            {
                                ID_FACTURA = factura.ID_FACTURA,
                                ID_PRODUCTO = idProducto[i],
                                CANTIDAD = cantidad[i],
                                PRECIO_UNIDAD = precioUnidad[i],
                                SUBTOTAL = subtotal
                            });
                        }
                    }

                    // Actualizar total
                    factura.TOTAL = totalFactura;
                    
                    CargarListas(model);
                    return View("gestionarFactura", model);

                    db.SaveChanges();
                }

                TempData["SuccessMessage"] = "Factura guardada correctamente.";
                return RedirectToAction("mantFacturas");
            }
            catch (Exception ex)
            {
                var mensajeCompleto = ex.Message;
                if (ex.InnerException != null)
                    mensajeCompleto += " | Inner: " + ex.InnerException.Message;
                if (ex.InnerException?.InnerException != null)
                    mensajeCompleto += " | Inner2: " + ex.InnerException.InnerException.Message;

                TempData["ErrorMessage"] = "Error al guardar: " + mensajeCompleto;
                CargarListas(model);
                return View("gestionarFactura", model);
            }
        }

        // ============================================================
        //  Acciones originales (se mantienen)
        // ============================================================

        [HttpGet]
        public ActionResult agregarFactura()
        {
            var model = new CFacturaCompleta(); // 👈 usa el completo mejor

            using (var db = new LaFarmaciaEntities())
            {
                CargarListas(model, db);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult agregarFactura(CAgregarFactura model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    using (var db = new LaFarmaciaEntities())
                    {
                        model.Clientes = db.CLIENTE.Select(c => new SelectListItem
                        {
                            Value = c.ID_CLIENTE.ToString(),
                            Text = c.NOMBRE + " " + c.APELLIDO
                        }).ToList();

                        
                    }
                    return View(model);


                    
                }
                using (var db = new LaFarmaciaEntities())
                {
                    FACTURAS nuevo = new FACTURAS
                    {
                        FECHA = model.fecha,
                        METODO_PAGO = model.metodoPago,
                        TOTAL = model.total,
                        ID_CLIENTE = model.idCliente
                    };
                    db.FACTURAS.Add(nuevo);
                    db.SaveChanges();
                }
                return RedirectToAction("mantFacturas");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al agregar la factura: " + ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult actualizarFactura(int id)
        {
            CActualizarFactura model = new CActualizarFactura();
            using (var db = new LaFarmaciaEntities())
            {
                var factura = db.FACTURAS.Find(id);

                if (factura == null) return HttpNotFound();

                model.idFactura = factura.ID_FACTURA;
                model.fecha = factura.FECHA;
                model.metodoPago = factura.METODO_PAGO;
                model.total = factura.TOTAL;
                model.idCliente = factura.ID_CLIENTE;

                model.Clientes = db.CLIENTE.Select(c => new SelectListItem
                {
                    Value = c.ID_CLIENTE.ToString(),
                    Text = c.NOMBRE + " " + c.APELLIDO
                }).ToList();

                model.Productos = db.PRODUCTO.Select(p => new SelectListItem
                {
                    Value = p.ID_PRODUCTO.ToString(),
                    Text = p.NOMBRE
                }).ToList();

                
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult actualizarFactura(CActualizarFactura model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    using (var db = new LaFarmaciaEntities())
                    {
                        model.Clientes = db.CLIENTE.Select(c => new SelectListItem
                        {
                            Value = c.ID_CLIENTE.ToString(),
                            Text = c.NOMBRE + " " + c.APELLIDO
                        }).ToList();
                    }
                    return View(model);
                }
                using (var db = new LaFarmaciaEntities())
                {
                    var factura = db.FACTURAS.Find(model.idFactura);
                    if (factura == null) return HttpNotFound();
                    factura.FECHA = model.fecha;
                    factura.METODO_PAGO = model.metodoPago;
                    factura.TOTAL = model.total;
                    factura.ID_CLIENTE = model.idCliente;
                    db.Entry(factura).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("mantFacturas");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar la factura: " + ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult eliminarFactura(int id)
        {
            using (var db = new LaFarmaciaEntities())
            {
                var factura = db.FACTURAS.Find(id);
                if (factura == null) return HttpNotFound();
                db.FACTURAS.Remove(factura);
                db.SaveChanges();
            }
            return RedirectToAction("mantFacturas");
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var db = new LaFarmaciaEntities())
                {
                    var factura = db.FACTURAS.Find(id);
                    if (factura == null) return HttpNotFound();
                    db.FACTURAS.Remove(factura);
                    db.SaveChanges();
                }
                return RedirectToAction("mantFacturas");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar la factura: " + ex.Message);
                return RedirectToAction("mantFacturas");
            }
        }

        // ============================================================
        //  Helpers privados
        // ============================================================
        private void CargarListas(CFacturaCompleta model, LaFarmaciaEntities db = null)
        {
            bool disponer = db == null;
            if (db == null) db = new LaFarmaciaEntities();
            try
            {
                model.Clientes = db.CLIENTE.Select(c => new SelectListItem
                {
                    Value = c.ID_CLIENTE.ToString(),
                    Text = c.NOMBRE + " " + c.APELLIDO
                }).ToList();

           
                model.Productos = db.PRODUCTO

                    .Select(p => new SelectListItem
                    {
                        Value = p.ID_PRODUCTO.ToString(),
                        Text = p.NOMBRE + " [" + p.PRECIO + "]"  
                    }).ToList();
            }
            finally
            {
                if (disponer) db.Dispose();
            }
        
        }
    }
}
