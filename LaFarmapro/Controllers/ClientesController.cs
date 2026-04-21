
using LaFarmapro;
using LaFarmapro.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaFarma.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult mantClientes()
        {
            List<CListaClientes> lista = null;

            using (var db = new LaFarmaciaEntities())
            {
                lista = (from c in db.CLIENTE
                         select new CListaClientes
                         {
                             idCliente = c.ID_CLIENTE,
                            identificacionCliente = c.IDENTIFICACION,
                             nombreCliente = c.NOMBRE,
                             apellidoCliente = c.APELLIDO,
                             correoCliente = c.CORREO
                         }).ToList();
            }

            return View(lista);
        }

        // GET: Clientes/Details/5
        public ActionResult consultarCliente(int id)
        {
            CListaClientes model = new CListaClientes();
            using (LaFarmaciaEntities db = new LaFarmaciaEntities())
            {
                var cliente = db.CLIENTE.Find(id);
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                model.idCliente = cliente.ID_CLIENTE;
                model.identificacionCliente = cliente.IDENTIFICACION;
                model.nombreCliente = cliente.NOMBRE;
                model.apellidoCliente = cliente.APELLIDO;
                model.correoCliente = cliente.CORREO;
            }
            return View(model);
        }

        // GET: Clientes/Create
        [HttpGet]
        public ActionResult agregarCliente()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult agregarCliente(CAgregarCliente model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("agregarCliente", model);
                }

                using (LaFarmaciaEntities db = new LaFarmaciaEntities())
                {
                    CLIENTE nuevo = new CLIENTE
                    {
                        IDENTIFICACION = model.identificacionCliente,
                        NOMBRE = model.nombreCliente,
                        APELLIDO = model.apellidoCliente,
                        CORREO = model.correoCliente
                    };
                    db.CLIENTE.Add(nuevo);
                    db.SaveChanges();
                }
                return RedirectToAction("mantClientes");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al agregar el cliente: " + ex.Message);
                return View(model);
            }
        }

        // GET: Clientes/Edit/5
        [HttpGet]
        public ActionResult actualizarCliente(int id)
        {
            using (LaFarmaciaEntities db = new LaFarmaciaEntities())
            {
                var cliente = db.CLIENTE.Find(id);
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                    CActualizarCliente model = new CActualizarCliente
                {
                    idCliente = cliente.ID_CLIENTE,
                    identificacionCliente = cliente.IDENTIFICACION,
                    nombreCliente = cliente.NOMBRE,
                    apellidoCliente = cliente.APELLIDO,
                    correoCliente = cliente.CORREO
                };
                return View(model);
            }
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult actualizarCliente(CActualizarCliente model)
        {
            try
            {
                using (LaFarmaciaEntities db = new LaFarmaciaEntities())
                {
                    var cliente = db.CLIENTE.Find(model.idCliente);
                    if (cliente == null)
                    {
                        return HttpNotFound();
                    }
                    cliente.IDENTIFICACION = model.identificacionCliente;
                    cliente.NOMBRE = model.nombreCliente;
                    cliente.APELLIDO = model.apellidoCliente;
                    cliente.CORREO = model.correoCliente;

                    db.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("mantClientes");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar el cliente: " + ex.Message);
                return View(model);
            }
        }

        // GET: Clientes/Delete/5
        [HttpGet]
        public ActionResult eliminarCliente(int id)
        {
            using (LaFarmaciaEntities db = new LaFarmaciaEntities())
            {
                var cliente = db.CLIENTE.Find(id);
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                db.CLIENTE.Remove(cliente);
                db.SaveChanges();
            }
            return RedirectToAction("mantClientes");
        }

        // POST: Clientes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (LaFarmaciaEntities db = new LaFarmaciaEntities())
                {
                    var cliente = db.CLIENTE.Find(id);
                    if (cliente == null)
                    {
                        return HttpNotFound();
                    }
                    db.CLIENTE.Remove(cliente);
                    db.SaveChanges();
                }
                return RedirectToAction("mantClientes");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el cliente: " + ex.Message);
                return RedirectToAction("mantClientes");
            }
        }
    }
}
