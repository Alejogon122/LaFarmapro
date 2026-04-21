using LaFarmapro;
using LaFarmapro.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace LaFarma.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult mantUsuarios()

        {
            List<CListaUsuario> listaUsuarios = null;

            using (var db = new LaFarmaciaEntities())
            {
                listaUsuarios = (from u in db.USUARIO
                                 
                                 select new CListaUsuario
                                 {
                                     idUsuario = u.ID_USUARIO,
                                     identificacionUsuario = u.IDENTIFICACION,
                                     nombreUsuario = u.NOMBRE,
                                     apellidoUsuario = u.APELLIDO,
                                     correoUsuario = u.CORREO,
                                     tipoUsuario = u.TIPO_USUARIO,
                                     estadoUsuario = u.ESTADO
                                 }).ToList();
            }
            return View(listaUsuarios);
        }


        [HttpGet]
        public ActionResult agregarUsuario()
        {
            return View();
        }


        [HttpPost]

        public ActionResult agregarUsuario(CAgregarUsuario model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("agregarUsuario", model);
                }   

                using (LaFarmaciaEntities db= new LaFarmaciaEntities())
                {
                    USUARIO nuevoUsuario = new USUARIO
                    {
                        IDENTIFICACION = model.identificacionUsuario,
                        NOMBRE = model.nombreUsuario,
                        APELLIDO = model.apellidoUsuario,
                        CORREO = model.correoUsuario,
                        TIPO_USUARIO = model.tipoUsuario,
                        ESTADO = model.estadoUsuario
                    };
                    db.USUARIO.Add(nuevoUsuario);
                    db.SaveChanges();
                }
                return RedirectToAction("mantUsuarios");

            }

            catch (Exception ex)
            {
                // Manejar la excepción (puedes registrar el error o mostrar un mensaje)
                ModelState.AddModelError("", "Error al agregar el usuario: " + ex.Message);
                return View(model);
            }
        }


        [HttpGet]
        public ActionResult actualizarUsuario(int id)
        {
            using (LaFarmaciaEntities db = new LaFarmaciaEntities())
            {
                var usuario = db.USUARIO.Find(id);

                if (usuario == null)
                {
                    return HttpNotFound();
                }
                CActualizarUsuario model = new CActualizarUsuario
                {
                    idUsuario = usuario.ID_USUARIO,
                    identificacionUsuario = usuario.IDENTIFICACION,
                    nombreUsuario = usuario.NOMBRE,
                    apellidoUsuario = usuario.APELLIDO,
                    correoUsuario = usuario.CORREO,
                    tipoUsuario = usuario.TIPO_USUARIO,
                    estadoUsuario = usuario.ESTADO
                };
                return View(model);
            }
        }



        [HttpPost]
        public ActionResult actualizarUsuario(CActualizarUsuario model)
        {
            try
            {
                using (var db = new LaFarmaciaEntities())
                {
                    var usuario = db.USUARIO.Find(model.idUsuario);
                    if (usuario == null)
                    {
                        return HttpNotFound();
                    }
                    usuario.IDENTIFICACION = model.identificacionUsuario;
                    usuario.NOMBRE = model.nombreUsuario;
                    usuario.APELLIDO = model.apellidoUsuario;
                    usuario.CORREO = model.correoUsuario;
                    usuario.TIPO_USUARIO = model.tipoUsuario;
                    usuario.ESTADO = model.estadoUsuario;
                    db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Usuario actualizado exitosamente.";
                }
                return RedirectToAction("mantUsuarios");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar el usuario: " + ex.Message);
                return View(model);
            }
        }


        [HttpGet]

        public ActionResult consultarUsuario (int id)
        {
            CListaUsuario usuario = new CListaUsuario();
            using (LaFarmaciaEntities db = new LaFarmaciaEntities())
                {

                var consulta = db.USUARIO.Find(id);
                if (consulta != null)
                {
                    usuario.idUsuario = consulta.ID_USUARIO;
                    usuario.identificacionUsuario = consulta.IDENTIFICACION;
                    usuario.nombreUsuario = consulta.NOMBRE;
                    usuario.apellidoUsuario = consulta.APELLIDO;
                    usuario.correoUsuario = consulta.CORREO;
                    usuario.tipoUsuario = consulta.TIPO_USUARIO;
                    usuario.estadoUsuario = consulta.ESTADO;
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return View(usuario);
        }



        

        
        [HttpGet]
        public ActionResult eliminarUsuario(int id )
        {
           using (LaFarmaciaEntities db = new LaFarmaciaEntities())
            {
                var usuario = db.USUARIO.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                db.USUARIO.Remove(usuario);

                db.SaveChanges();
            }
            return RedirectToAction("mantUsuarios");
        }
    }
}
