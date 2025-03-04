using EjercicioCRUD.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult ExportarPDF()
        {
            var items = _repository.ObtenerItems(1, int.MaxValue);

            Document doc = new Document(PageSize.A4, 20, 20, 30, 50);
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            writer.PageEvent = new PDFNumPag(); 

            doc.Open();

            
            Paragraph encabezado = new Paragraph("Reporte de Ítems", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
            encabezado.Alignment = Element.ALIGN_CENTER;
            doc.Add(encabezado);
            doc.Add(new Paragraph($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}\n\n"));

            
            PdfPTable tabla = new PdfPTable(5);
            tabla.WidthPercentage = 100;
            float[] columnWidths = { 10f, 30f, 20f, 20f, 20f };
            tabla.SetWidths(columnWidths);

            
            string[] headers = { "ID", "Código", "Descripción", "Precio", "Cantidad" };
            foreach (string header in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(header, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.AddCell(cell);
            }

            
            foreach (var item in items)
            {
                tabla.AddCell(item.ID.ToString());
                tabla.AddCell(item.CodigoBusqueda);
                tabla.AddCell(item.Descripcion);
                tabla.AddCell(item.Precio.ToString("C"));
                tabla.AddCell(item.Cantidad.ToString());
            }

            doc.Add(tabla);
            doc.Close();

            return File(memoryStream.ToArray(), "application/pdf", "Reporte_Items.pdf");
        }
    }
}