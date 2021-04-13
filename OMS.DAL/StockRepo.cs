using OMS.Domain;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OMS.DAL
{
    public class StockRepo
    {
        public List<StockItem> GetStockItems()
        {
            var items = new List<StockItem>();
            using (var conn = new SqlConnection(OMDB.ConnectionString))
            using (var command = new SqlCommand("sp_SelectStockItems", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                conn.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(new StockItem(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetDecimal(2),
                        reader.GetInt32(3)
                    ));
                }
                conn.Close();
            }           
            return items;
        }

        public StockItem GetStockItem(int id)
        {
            StockItem item = null;
            using (var conn = new SqlConnection(OMDB.ConnectionString))
            using (var command = new SqlCommand("sp_SelectStockItemById", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                conn.Open();
                var reader = command.ExecuteReader(CommandBehavior.SingleRow);
                reader.Read();
                item = new StockItem(
                        id,
                        reader.GetString(1),
                        reader.GetDecimal(2),
                        reader.GetInt32(3)
                );                
                conn.Close();
            }
            return item;
        }

        public void UpdateStockItemAmount(OrderHeader order)
        {
            using (var conn = new SqlConnection(OMDB.ConnectionString))
            using (var command = new SqlCommand("sp_UpdateStockItemAmount", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                conn.Open();
                var transaction = conn.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    foreach (var item in order.OrderItems)
                    {                   
                        command.Parameters.AddWithValue("@id", item.StockItemId);
                        command.Parameters.AddWithValue("@amount", -item.Quantity);                        
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }                
            }
        }

        public void UpdateStockItem(int id, string name, decimal price, int inStock)
        {
            string commandText = "UPDATE StockItems SET Name = @name, Price = @price, InStock = @inStock WHERE Id = @id";
            using (var conn = new SqlConnection(OMDB.ConnectionString))
            using (var command = new SqlCommand(commandText, conn))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@inStock", inStock);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void InsertStockItem(string name, decimal price, int inStock)
        {
            string commandText = "INSERT INTO StockItems (Name, Price, InStock) VALUES (@name, @price, @inStock)";
            using (var conn = new SqlConnection(OMDB.ConnectionString))
            using (var command = new SqlCommand(commandText, conn))
            {            
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@inStock", inStock);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }

        }
    }
}
