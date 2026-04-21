
using LaFarmapro;
using LaFarmapro.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaFarma.Controllers
{
    public class ProductosController : Controller
    {
        // GET: Productos
        public ActionResult mantProductos()
        {
            List<CListaProductos> listaProductos = null;

            using (var db = new LaFarmaciaEntities())
            {
                listaProductos = (from p in db.PRODUCTO
                                  select new CListaProductos
                                  {
                                      idProducto = p.ID_PRODUCTO,
                                      nombreProducto = p.NOMBRE,
                                      descripcionProducto = p.DESCRIPCION,
                                      precioProducto = p.PRECIO,
                                      estadoProducto = p.ESTADO
                                  }).ToList();

            }


            return View(listaProductos);
        }


        [HttpGet]
        public ActionResult agregarProducto()

        {
            return View();
        }



        [HttpPost]
        public ActionResult agregarProducto(CAgregarProducto model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View("agregarProducto", model);
                }
                using (var db = new LaFarmaciaEntities())
                {
                    PRODUCTO nuevoProducto = new PRODUCTO
                    {
                        
                        NOMBRE = model.nombreProducto,
                        DESCRIPCION = model.descripcionProducto,
                        PRECIO = model.precioProducto,
                        ESTADO = model.estadoProducto
                    };
                    db.PRODUCTO.Add(nuevoProducto);
                    db.SaveChanges();
                }
                return RedirectToAction("mantProductos");
            }
            catch (Exception ex)
            {
                {
                    ViewBag.Error = ex;

                    return View();
                }
            }

        }


        [HttpGet]
        public ActionResult actualizarProducto(int id)

        {
            using (LaFarmaciaEntities db = new LaFarmaciaEntities())
            {
                var producto = db.PRODUCTO.Find(id);

                CActualizarProducto model = new CActualizarProducto
                {
                    idProducto = producto.ID_PRODUCTO,
                   
                    nombreProducto = producto.NOMBRE,
                    descripcionProducto = producto.DESCRIPCION,
                    precioProducto = producto.PRECIO,
                    estadoProducto = producto.ESTADO
                };
                return View(model);
            }
        }


        [HttpPost]
        public ActionResult actualizarProducto(CActualizarProducto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }


                using (LaFarmaciaEntities db = new LaFarmaciaEntities())
                {
                    var producto = db.PRODUCTO.Find(model.idProducto);

                    if (producto == null)
                    {
                        return HttpNotFound();
                    }

                    
                    producto.NOMBRE = model.nombreProducto;
                    producto.DESCRIPCION = model.descripcionProducto;
                    producto.PRECIO = model.precioProducto;
                    producto.ESTADO = model.estadoProducto;

                    db.Entry(producto).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Producto actualizado exitosamente.";
                }
                return RedirectToAction("mantProductos");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar el producto: " + ex.Message);
                return View(model);
            }
        }



        public ActionResult consultarProducto(int id)
        {
            CListaProductos producto = new CListaProductos();
            using (LaFarmaciaEntities db = new LaFarmaciaEntities())
            {
                var consulta = db.PRODUCTO.Find(id);
                if (producto == null)
                {
                    return HttpNotFound();
                }
                producto.idProducto = consulta.ID_PRODUCTO;
                producto.nombreProducto = consulta.NOMBRE;
                producto.descripcionProducto = consulta.DESCRIPCION;
                producto.precioProducto = consulta.PRECIO;
                producto.estadoProducto = consulta.ESTADO;
            }
            return View(producto);

        }



        public ActionResult eliminarProducto(int id)
        {
            try
            {
                using (LaFarmaciaEntities db = new LaFarmaciaEntities())
                {
                    var producto = db.PRODUCTO.Find(id);

                    if (producto == null)
                    {
                        return HttpNotFound();
                    }

                    db.PRODUCTO.Remove(producto);

                    db.SaveChanges();
                }
                return RedirectToAction("mantProductos");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el producto: " + ex.Message);
                return RedirectToAction("mantProductos");
            }
        }
    }
}