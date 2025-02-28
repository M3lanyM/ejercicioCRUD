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

        public ActionResult Index()
        {
            var items = _repository.ObtenerItems();
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
            var item = _repository.ObtenerItems().Find(x => x.ID == id);
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
    }
}