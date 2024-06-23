using ProductionOrder_WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace ProductionOrder_WebAPI.DAL
{
    public class ProdOrderRepository : IProdOrderRepository
    {
        private readonly IConfiguration Configuration;

        string connString = string.Empty;

        public ProdOrderRepository(IConfiguration configuration)
        {  
            Configuration = configuration;

            connString = Configuration.GetValue<string>("DB:ConnectionString");
        }
        public void AddProdOrder(Order prodOrder)
        {
            //prodOrder.Status = "Scheduled";

            DateTimeOffset createdDT = DateTimeOffset.Now;
            DateTimeOffset modifiedDT = createdDT;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand("WO_Add", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter { ParameterName = "@WONumber", SqlDbType = SqlDbType.NVarChar, Value = prodOrder.OrderNumber });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@WOStatus", SqlDbType = SqlDbType.NVarChar, Value = prodOrder.Status });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@PartID", SqlDbType = SqlDbType.NVarChar, Value = prodOrder.Material });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@Quantity", SqlDbType = SqlDbType.Float, Value = prodOrder.Quantity });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@UnitOfMeasure", SqlDbType = SqlDbType.NVarChar, Value = prodOrder.Uom });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@CreatedDT", SqlDbType = SqlDbType.DateTimeOffset, Value = createdDT });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@ModifiedDT", SqlDbType = SqlDbType.DateTimeOffset, Value = modifiedDT });

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public List<Plant> GetPlantLocations()
        {
            List<Plant> plants = new List<Plant>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand("GetPlants", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    while (reader.Read()) 
                    { 
                        var plant = new Plant();

                        plant.PlantID = Convert.ToInt32(reader["PlantID"]);
                        plant.PlantName = reader["PlantName"].ToString();
                        plant.PlantLocation = reader["PlantLocation"].ToString();

                        plants.Add(plant);
                    }
                }

                connection.Close();
            }

            return plants;

        }
        public List<UOM> GetUOM()
        {
            List<UOM> units = new List<UOM>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand("GetUnits", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    while (reader.Read())
                    {
                        var uom = new UOM();

                        uom.UnitID = Convert.ToInt32(reader["UnitID"]);
                        uom.UnitName = reader["UnitName"].ToString();
                        uom.UnitAbbr = reader["UnitAbbr"].ToString();

                        units.Add(uom);
                    }
                }

                connection.Close();
            }

            return units;

        }

        public List<Material> GetMaterials()
        {
            List<Material> materials = new List<Material>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand("GetMaterials", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    while (reader.Read())
                    {
                        var material = new Material();
                        material.MaterialID = Convert.ToInt32(reader["MaterialID"]);
                        material.MaterialNumber = reader["MaterialNumber"].ToString();
                        material.MaterialName = reader["MaterialName"].ToString();

                        materials.Add(material);
                    }
                }

                connection.Close();
            }

            return materials;

        }

        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand("GetOrders", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    while (reader.Read())
                    {
                        var order = new Order();
                        order.OrderNumber = reader["WONumber"].ToString();
                        order.Material = reader["PartID"].ToString();
                        order.Quantity = Convert.ToDouble(reader["Quantity"]);
                        order.Uom = reader["UnitOfMeasure"].ToString();

                        orders.Add(order);
                    }
                }

                connection.Close();
            }

            return orders;

        }
    }
}
