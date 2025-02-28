using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EjercicioCRUD.Models
{
    public class ItemRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public List<Item> ObtenerItems(int pageNumber, int pageSize)
        {
            List<Item> items = new List<Item>();

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("prc_ObtenerItems", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                conexion.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    items.Add(new Item
                    {
                        ID = Convert.ToInt32(lector["ID"]),
                        CodigoBusqueda = lector["ItemLookupCode"].ToString(),
                        Descripcion = lector["Description"].ToString(),
                        Precio = Convert.ToDecimal(lector["Price"]),
                        Costo = Convert.ToDecimal(lector["Cost"]),
                        Cantidad = Convert.ToInt32(lector["Quantity"]),
                        IDProveedor = Convert.ToInt32(lector["SupplierID"]),
                        UltimaVenta = lector["LastSold"] != DBNull.Value ? Convert.ToDateTime(lector["LastSold"]) : (DateTime?)null,
                        UltimaActualizacion = Convert.ToDateTime(lector["LastUpdated"])
                    });
                }
            }
            return items;
        }


        public void InsertarItem(Item item)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("prc_InsertarItem", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemLookupCode", item.CodigoBusqueda);
                cmd.Parameters.AddWithValue("@Description", item.Descripcion);
                cmd.Parameters.AddWithValue("@Price", item.Precio);
                cmd.Parameters.AddWithValue("@Cost", item.Costo);
                cmd.Parameters.AddWithValue("@Quantity", item.Cantidad);
                cmd.Parameters.AddWithValue("@SupplierID", item.IDProveedor);
                cmd.Parameters.AddWithValue("@LastSold", (object)item.UltimaVenta ?? DBNull.Value);
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EditarItem(Item item)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("prc_EditarItem", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", item.ID);
                cmd.Parameters.AddWithValue("@ItemLookupCode", item.CodigoBusqueda);
                cmd.Parameters.AddWithValue("@Description", item.Descripcion);
                cmd.Parameters.AddWithValue("@Price", item.Precio);
                cmd.Parameters.AddWithValue("@Cost", item.Costo);
                cmd.Parameters.AddWithValue("@Quantity", item.Cantidad);
                cmd.Parameters.AddWithValue("@SupplierID", item.IDProveedor);
                cmd.Parameters.AddWithValue("@LastSold", (object)item.UltimaVenta ?? DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarItem(int id)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("prc_EliminarItem", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
