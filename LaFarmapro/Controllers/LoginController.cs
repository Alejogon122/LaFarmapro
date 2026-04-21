using LaFarmapro;
using LaFarmapro.Models.viewmodels;
using LaFarmapro.Models.viewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LaFarma.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpGet]
        public ActionResult Ingresar()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Ingresar(CLogin login)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }

            using (LaFarmaciaEntities db = new LaFarmaciaEntities())
            {
                var existenciaUsuario = db.USER
                    .Where(p => p.USER1 == login.user && p.PASSWORD == login.password)
                    .FirstOrDefault();

                if (existenciaUsuario != null)
                {
                    if (existenciaUsuario.ROL == "ADMINISTRADOR")
                    {
                        Session["ROL"] = existenciaUsuario.ROL;
                        Session["USER"] = existenciaUsuario.USER1;
                        Session.Timeout = 45;
                        return RedirectToAction("Index", "Home");
                    }
                    else if (existenciaUsuario.ROL == "VENDEDOR")
                    {
                        Session["ROL"] = existenciaUsuario.ROL;
                        Session["USER"] = existenciaUsuario.USER1;
                        Session.Timeout = 45;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ValorMensaje = 0;
                        ViewBag.MensajeProceso = "Su rol no tiene acceso al sistema.";
                        return View("Login");
                    }
                }
                else
                {
                    ViewBag.ValorMensaje = 0;
                    ViewBag.MensajeProceso = "Usuario o contraseña incorrectos.";
                    return View("Login");
                }
            }
        }

        // Cerrar sesión
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}
