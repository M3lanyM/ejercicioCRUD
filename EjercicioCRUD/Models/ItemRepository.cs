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

        public List<Item> ObtenerItems()
        {
            List<Item> items = new List<Item>();

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("prc_ObtenerItems", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
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
                        Inactivo = lector["Inactive"] != DBNull.Value && Convert.ToBoolean(lector["Inactive"]),
                        UltimaActualizacion = Convert.ToDateTime(lector["LastUpdated"])
                    });

                    // Depuración: Verificar que el campo Inactivo se está leyendo correctamente
                    Console.WriteLine($"ID: {lector["ID"]}, Inactivo: {lector["Inactive"]}");
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
                cmd.Parameters.AddWithValue("@Inactive", item.Inactivo);
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
                cmd.Parameters.AddWithValue("@Inactive", item.Inactivo);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
