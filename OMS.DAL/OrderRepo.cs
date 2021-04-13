using OMS.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace OMS.DAL
{
    public class OrderRepo
    {
        public OrderHeader InsertOrderHeader()
        {
            OrderHeader order = null;
            using (var connection = new SqlConnection(OMDB.ConnectionString))
            using (var command = new SqlCommand("sp_InsertOrderHeader", connection))
            {
                connection.Open();
                int id = Convert.ToInt32(command.ExecuteScalar());
                command.CommandText = "SELECT * FROM OrderHeaders WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                var reader = command.ExecuteReader(CommandBehavior.SingleRow);
                reader.Read();
                var datetime = reader.GetDateTime(2);
                reader.Close();
                connection.Close();
                order = new OrderHeader(id, datetime, 1);
            }
            return order;
        }

        public IEnumerable<OrderHeader> GetOrderHeaders()
        {
            var orders = new List<OrderHeader>();
            OrderHeader order = null;
            using (var connection = new SqlConnection(OMDB.ConnectionString))
            using (var command = new SqlCommand("sp_SelectOrderHeaders", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int orderId = reader.GetInt32(0);
                    if (order == null || order.Id != orderId)
                    {                        
                        order = new OrderHeader(
                            orderId,
                            reader.GetDateTime(2),
                            reader.GetInt32(1)
                        );
                        orders.Add(order);
                    }
                    if (!reader.IsDBNull(3))
                    {
                        order.AddOrderItem(
                            reader.GetInt32(3),
                            reader.GetDecimal(5),
                            reader.GetString(4),
                            reader.GetInt32(6)
                        );
                    }
                }
                reader.Close();
                connection.Close();
            }

            return orders.AsEnumerable();
        }

        public OrderHeader GetOrderHeader(int id)
        {
            OrderHeader order = null;
            using (var connection = new SqlConnection(OMDB.ConnectionString))
            using (var command = new SqlCommand("sp_SelectOrderHeaderById", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (order == null)
                    {
                        order = new OrderHeader(
                            id,
                            reader.GetDateTime(2),
                            reader.GetInt32(1)
                        );
                    }
                    if (!reader.IsDBNull(3))
                    {
                        order.AddOrderItem(
                            reader.GetInt32(3),
                            reader.GetDecimal(5),
                            reader.GetString(4),
                            reader.GetInt32(6)
                        );
                    }
                }                           
                reader.Close();
                connection.Close();
            }
            return order;
        }

        public void UpsertOrderItem(OrderItem item)
        {
            using (var connection = new SqlConnection(OMDB.ConnectionString))
            using (var command = new SqlCommand("sp_UpsertOrderItem", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@orderHeaderId", item.OrderHeaderId);
                command.Parameters.AddWithValue("@stockItemId", item.StockItemId);
                command.Parameters.AddWithValue("@description", item.Description);
                command.Parameters.AddWithValue("@price", item.Price);
                command.Parameters.AddWithValue("@quantity", item.Quantity);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateOrderState(OrderHeader order)
        {
            using (var connection = new SqlConnection(OMDB.ConnectionString))
            using (var command = new SqlCommand("sp_UpdateOrderState", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@orderHeaderId", order.Id);
                command.Parameters.AddWithValue("@stateId", (int)order.State);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        
        public void DeleteOrderHeaderAndOrderItems(int orderHeaderId)
        {
            using (var connection = new SqlConnection(OMDB.ConnectionString))
            using (var command = new SqlCommand("sp_DeleteOrderHeaderAndOrderItems", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@orderHeaderId", orderHeaderId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteOrderItem(int orderHeaderId, int stockItemId)
        {
            using (var connection = new SqlConnection(OMDB.ConnectionString))
            using (var command = new SqlCommand("sp_DeleteOrderItem", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@orderHeaderId", orderHeaderId);
                command.Parameters.AddWithValue("@stockItemId", stockItemId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void ResetDatabase()
        {
            using (var connection = new SqlConnection(OMDB.ConnectionString))
            using (var command = new SqlCommand("sp_ResetDatabase", connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
