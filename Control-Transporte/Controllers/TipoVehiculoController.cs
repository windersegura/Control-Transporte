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
    public class TipoVehiculoController : ApiController
    {
        public IHttpActionResult Get()
        {
            List<TipoVehiculoModel> output = new List<TipoVehiculoModel>();
            StringBuilder query = new StringBuilder();
            query.Append("GetTipoVehiculo");

            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query.ToString(), connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        output.Add(new TipoVehiculoModel
                        {
                            iidTipo = int.Parse(reader["id_tipo"].ToString()),
                            nombre_tipo = reader["nombre_tipo"].ToString()
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
    }
}
