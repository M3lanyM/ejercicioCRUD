using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EjercicioCRUD.Controllers
{
        public class CuentaController : Controller
        {
            public ActionResult IniciarSesion()
            {
                return View();
            }

            [HttpPost]
            public ActionResult IniciarSesion(string usuario, string clave)
            {
                // No importa qué ingrese, lo dejamos pasar
                Session["Usuario"] = usuario ?? "CualquierCosa";
                return RedirectToAction("Index", "Item");
            }

            public ActionResult CerrarSesion()
            {
                Session["Usuario"] = null;
                return RedirectToAction("IniciarSesion");
            }
        }
    }
