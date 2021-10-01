using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;
using Control_Transporte.Models;

namespace Control_Transporte.Controllers
{
    public class VehiculoController : ApiController
    {
        //GET
        public IHttpActionResult Get()
        {
            List<VehiculoModel> output = new List<VehiculoModel>();
            StringBuilder query = new StringBuilder();
            query.Append("GetVehiculos");

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query.ToString(), connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        output.Add(new VehiculoModel
                        {
                            iidVehiculo = int.Parse(reader["id_vehiculo"].ToString()),
                            capacidad = double.Parse(reader["capacidad"].ToString()),
                            consumo_combustible = double.Parse(reader["consumo_conbustible"].ToString()),
                            costo_depreciacion = double.Parse(reader["costo_depreciacion"].ToString()),
                            modelo = reader["modelo"].ToString(),
                            iidPiloto = int.Parse(reader["id_piloto"].ToString()),
                            iidTipo = int.Parse(reader["id_tipo"].ToString()),
                            nombre_piloto = reader["nombre_apellido"]?.ToString(),
                            tipo_vehiculo = reader["nombre_tipo"]?.ToString()
                        });
                    }
                }
                connection.Close();
            }

            if (output.Count == 0)
            {
                return NotFound();
            }

            return Ok(output);
        }

        public IHttpActionResult GetEmpleadoById(int id)
        {
            VehiculoModel output = null;

            StringBuilder query = new StringBuilder();
            query.Append("GetVehiculoById");

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query.ToString(), connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        output = new VehiculoModel
                        {
                            iidVehiculo = int.Parse(reader["id_vehiculo"].ToString()),
                            capacidad = double.Parse(reader["capacidad"].ToString()),
                            consumo_combustible = double.Parse(reader["consumo_conbustible"].ToString()),
                            costo_depreciacion = double.Parse(reader["costo_depreciacion"].ToString()),
                            modelo = reader["modelo"].ToString(),
                            iidPiloto = int.Parse(reader["id_piloto"].ToString()),
                            iidTipo = int.Parse(reader["id_tipo"].ToString())
                        };
                    }
                }
                connection.Close();
            }
            if (output == null)
            {
                return NotFound();
            }

            return Ok(output);
        }

        //POST 
        public IHttpActionResult Post(VehiculoModel obj)
        {
            StringBuilder query = new StringBuilder();
            query.Append("AddVehiculo");

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query.ToString(), connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Capacidad", obj.capacidad);
                cmd.Parameters.AddWithValue("@Consumo", obj.consumo_combustible);
                cmd.Parameters.AddWithValue("@CostoDepreciacion", obj.costo_depreciacion);
                cmd.Parameters.AddWithValue("@Modelo", obj.modelo);
                cmd.Parameters.AddWithValue("@Tipo", obj.iidTipo);
                cmd.Parameters.AddWithValue("@Piloto", obj.iidPiloto);

                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();

                if (i >= 1)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
        }


        //PUT 
        public IHttpActionResult Put(VehiculoModel obj)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UpdateVehiculo");
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query.ToString(), connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdVehiculo", obj.iidVehiculo);
                cmd.Parameters.AddWithValue("@Capacidad", obj.capacidad);
                cmd.Parameters.AddWithValue("@Consumo", obj.consumo_combustible);
                cmd.Parameters.AddWithValue("@CostoDepreciacion", obj.costo_depreciacion);
                cmd.Parameters.AddWithValue("@Modelo", obj.modelo);
                cmd.Parameters.AddWithValue("@Tipo", obj.iidTipo);
                cmd.Parameters.AddWithValue("@Piloto", obj.iidPiloto);

                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();

                if (i >= 1)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
        }


        //DELETE
        public IHttpActionResult Delete(int id)
        {
            StringBuilder query = new StringBuilder();
            query.Append("DeleteVehiculoById");
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query.ToString(), connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);


                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();

                if (i >= 1)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }

        }
    }
}
