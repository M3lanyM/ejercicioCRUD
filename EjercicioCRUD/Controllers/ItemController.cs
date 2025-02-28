using EjercicioCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EjercicioCRUD.Controllers
{
    public class ItemController : Controller
    {
        private ItemRepository _repository = new ItemRepository();

        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("IniciarSesion", "Cuenta");

            var items = _repository.ObtenerItems(page, pageSize);
            ViewBag.PageNumber = page;
            ViewBag.PageSize = pageSize;
            return View(items);
        }


        public ActionResult CrearNuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearNuevo(Item item)
        {
            if (ModelState.IsValid)
            {
                _repository.InsertarItem(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public ActionResult Editar(int id)
        {
            var item = _repository.ObtenerItems(1, 10).Find(x => x.ID == id);
            Console.WriteLine($"Editar: ID {item.ID}, Última Venta: {item.UltimaVenta?.ToString("yyyy-MM-ddTHH:mm")}");
            return View(item);
        }


        [HttpPost]
        public ActionResult Editar(Item item)
        {
            if (ModelState.IsValid)
            {
                _repository.EditarItem(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            _repository.EliminarItem(id);
            return RedirectToAction("Index");
        }
    }
}
