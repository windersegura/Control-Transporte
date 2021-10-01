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
    public class EmpleadoController : ApiController
    {
        //GET api/Empleado
        public IHttpActionResult Get()
        {
            List<EmpleadoModel> output = new List<EmpleadoModel>();
            StringBuilder query = new StringBuilder();
            query.Append("GetPilotos");

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query.ToString(), connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        output.Add(new EmpleadoModel
                        {
                            iidPiloto = int.Parse(reader["id_piloto"].ToString()),
                            nombre_apellido = reader["nombre_apellido"].ToString(),
                            numero = int.Parse(reader["numero_piloto"].ToString()),
                            viaticos_asignados = double.Parse(reader["viaticos_asignados"].ToString()),
                            costos_adicionales_asignados = double.Parse(reader["costos_adicionales_asignados"].ToString()),
                            id_tipo = int.Parse(reader["id_tipo"]?.ToString()),
                            nombre_tipo = reader["nombre_tipo"]?.ToString()
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
            EmpleadoModel output = null;

            StringBuilder query = new StringBuilder();
            query.Append("GetPilotoById");

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
                        output = new EmpleadoModel
                        {
                            iidPiloto = int.Parse(reader["id_piloto"].ToString()),
                            nombre_apellido = reader["nombre_apellido"].ToString(),
                            numero = int.Parse(reader["numero_piloto"].ToString()),
                            viaticos_asignados = double.Parse(reader["viaticos_asignados"].ToString()),
                            costos_adicionales_asignados = double.Parse(reader["costos_adicionales_asignados"].ToString()),
                            id_tipo = int.Parse(reader["id_tipo"].ToString())
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

        public IHttpActionResult Post(EmpleadoModel obj)
        {
            StringBuilder query = new StringBuilder();
            query.Append("AddPiloto");

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query.ToString(), connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombres", obj.nombre_apellido);
                cmd.Parameters.AddWithValue("@Numero", obj.numero);
                cmd.Parameters.AddWithValue("@Viaticos", obj.viaticos_asignados);
                cmd.Parameters.AddWithValue("@Costos", obj.costos_adicionales_asignados);
                cmd.Parameters.AddWithValue("@Tipo", obj.id_tipo);

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
        public IHttpActionResult Put(EmpleadoModel obj)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UpdatePiloto");

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query.ToString(), connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPiloto", obj.iidPiloto);
                cmd.Parameters.AddWithValue("@Nombres", obj.nombre_apellido);
                cmd.Parameters.AddWithValue("@Numero", obj.numero);
                cmd.Parameters.AddWithValue("@Viaticos", obj.viaticos_asignados);
                cmd.Parameters.AddWithValue("@Costos", obj.costos_adicionales_asignados);
                cmd.Parameters.AddWithValue("@Tipo", obj.id_tipo);

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
            query.Append("DeletePiloto");
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
