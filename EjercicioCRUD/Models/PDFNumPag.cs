using iTextSharp.text;
using iTextSharp.text.pdf;
using System;

namespace EjercicioCRUD.Models
{
    public class PDFNumPag : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable footer = new PdfPTable(1);
            footer.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            footer.DefaultCell.Border = Rectangle.NO_BORDER;

            PdfPCell cell = new PdfPCell(new Phrase("Página " + writer.PageNumber, new Font(Font.FontFamily.HELVETICA, 10)));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;

            footer.AddCell(cell);
            footer.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin, writer.DirectContent);
        }
    }
}
