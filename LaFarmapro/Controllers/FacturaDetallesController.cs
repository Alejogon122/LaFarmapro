
using LaFarmapro;
using LaFarmapro.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaFarma.Controllers
{
    public class FacturaDetallesController : Controller
    {
        // GET: FacturaDetalles
        public ActionResult mantFacturaDetalles()
        {
            List<CListaFacturaDetalles> lista = null;
            using (var db = new LaFarmaciaEntities())
            {
                lista = (from d in db.DETALLE_FACTURA
                         select new CListaFacturaDetalles
                         {
                             idFacturaDetalle = d.ID_DETALLE_FACTURA,
                             cantidad = d.CANTIDAD,
                             precioUnidad = d.PRECIO_UNIDAD,
                             subtotal = d.SUBTOTAL ?? 0,
                             
                         }).ToList();
            }
            return View(lista);
        }

        // GET: FacturaDetalles/Details/5
        public ActionResult consultarFacturaDetalle(int id)
        {
            CListaFacturaDetalles model = new CListaFacturaDetalles();
            using (var db = new LaFarmaciaEntities())
            {
                var detalle = db.DETALLE_FACTURA.Find(id);
                if (detalle == null)
                {
                    return HttpNotFound();
                }
                model.idFacturaDetalle = detalle.ID_DETALLE_FACTURA;
                model.cantidad = detalle.CANTIDAD;
                model.precioUnidad = detalle.PRECIO_UNIDAD;
                model.subtotal = detalle.SUBTOTAL ?? 0;
            }
            return View(model);
        }

        // GET: FacturaDetalles/Create
        [HttpGet]
        public ActionResult agregarFacturaDetalle()
        {
            CAgregarFacturaDetalle model = new CAgregarFacturaDetalle();
            using (var db = new LaFarmaciaEntities())
            {
                model.Facturas = db.FACTURAS.Select(f => new SelectListItem
                {
                    Value = f.ID_FACTURA.ToString(),
                    Text = f.ID_FACTURA.ToString()
                }).ToList();

                model.Productos = db.PRODUCTO.Select(p => new SelectListItem
                {
                    Value = p.ID_PRODUCTO.ToString(),
                    Text = p.ID_PRODUCTO + " - " + p.NOMBRE
                }).ToList();
            }
            return View(model);
        }

        // POST: FacturaDetalles/Create
        [HttpPost]
        public ActionResult agregarFacturaDetalle(CAgregarFacturaDetalle model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    using (var db = new LaFarmaciaEntities())
                    {
                        model.Facturas = db.FACTURAS.Select(f => new SelectListItem
                        {
                            Value = f.ID_FACTURA.ToString(),
                            Text = f.ID_FACTURA.ToString()
                        }).ToList();
                        model.Productos = db.PRODUCTO.Select(p => new SelectListItem
                        {
                            Value = p.ID_PRODUCTO.ToString(),
                            Text = p.ID_PRODUCTO + " - " + p.NOMBRE
                        }).ToList();
                    }
                    return View(model);
                }

                using (var db = new LaFarmaciaEntities())
                {
                    DETALLE_FACTURA nuevo = new DETALLE_FACTURA
                    {
                        ID_FACTURA = int.Parse(model.idFactura),
                        ID_PRODUCTO = model.idProducto,
                        CANTIDAD = model.cantidad,
                        PRECIO_UNIDAD = model.precioUnidad,
                        SUBTOTAL = model.subtotal
                    };
                    db.DETALLE_FACTURA.Add(nuevo);
                    db.SaveChanges();
                }
                return RedirectToAction("mantFacturaDetalles");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al agregar el detalle: " + ex.Message);
                return View(model);
            }
        }

        // GET: FacturaDetalles/Edit/5
        [HttpGet]
        public ActionResult actualizarFacturaDetalle(int id)
        {
            CActualizarFacturaDetalle model = new CActualizarFacturaDetalle();
            using (var db = new LaFarmaciaEntities())
            {
                var detalle = db.DETALLE_FACTURA.Find(id);
                if (detalle == null)
                {
                    return HttpNotFound();
                }
                model.idFacturaDetalle = detalle.ID_DETALLE_FACTURA;
                model.cantidad = detalle.CANTIDAD;
                model.precioUnidad = detalle.PRECIO_UNIDAD;
                model.subtotal = detalle.SUBTOTAL ?? 0;
                model.Facturas = db.FACTURAS.Select(f => new SelectListItem
                {
                    Value = f.ID_FACTURA.ToString(),
                    Text = f.ID_FACTURA.ToString()
                }).ToList();
                model.Productos = db.PRODUCTO.Select(p => new SelectListItem
                {
                    Value = p.ID_PRODUCTO.ToString(),
                    Text = p.ID_PRODUCTO + " - " + p.NOMBRE
                }).ToList();
            }
            return View(model);
        }

        // POST: FacturaDetalles/Edit/5
        [HttpPost]
        public ActionResult actualizarFacturaDetalle(CActualizarFacturaDetalle model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    using (var db = new LaFarmaciaEntities())
                    {
                        model.Facturas = db.FACTURAS.Select(f => new SelectListItem
                        {
                            Value = f.ID_FACTURA.ToString(),
                            Text = f.ID_FACTURA.ToString()
                        }).ToList();
                        model.Productos = db.PRODUCTO.Select(p => new SelectListItem
                        {
                            Value = p.ID_PRODUCTO.ToString(),
                            Text = p.ID_PRODUCTO + " - " + p.NOMBRE
                        }).ToList();
                    }
                    return View(model);
                }

                using (var db = new LaFarmaciaEntities())
                {
                    var detalle = db.DETALLE_FACTURA.Find(model.idFacturaDetalle);
                    if (detalle == null)
                    {
                        return HttpNotFound();
                    }
                    detalle.CANTIDAD = model.cantidad;
                    detalle.PRECIO_UNIDAD = model.precioUnidad;
                    detalle.SUBTOTAL = model.subtotal;

                    db.Entry(detalle).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("mantFacturaDetalles");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar el detalle: " + ex.Message);
                return View(model);
            }
        }

        // GET: FacturaDetalles/Delete/5
        [HttpGet]
        public ActionResult eliminarFacturaDetalle(int id)
        {
            using (var db = new LaFarmaciaEntities())
            {
                var detalle = db.DETALLE_FACTURA.Find(id);
                if (detalle == null)
                {
                    return HttpNotFound();
                }
                db.DETALLE_FACTURA.Remove(detalle);
                db.SaveChanges();
            }
            return RedirectToAction("mantFacturaDetalles");
        }

        // POST: FacturaDetalles/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var db = new LaFarmaciaEntities())
                {
                    var detalle = db.DETALLE_FACTURA.Find(id);
                    if (detalle == null)
                    {
                        return HttpNotFound();
                    }
                    db.DETALLE_FACTURA.Remove(detalle);
                    db.SaveChanges();
                }
                return RedirectToAction("mantFacturaDetalles");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el detalle: " + ex.Message);
                return RedirectToAction("mantFacturaDetalles");
            }
        }
    }
}
