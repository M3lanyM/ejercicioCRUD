using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EjercicioCRUD.Models
{
	public class Item
    {
        public int ID { get; set; }
        public string CodigoBusqueda { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Costo { get; set; }
        public int Cantidad { get; set; }
        public int IDProveedor { get; set; }
        public bool Inactivo { get; set; }
        public DateTime UltimaActualizacion { get; set; }
    }
}