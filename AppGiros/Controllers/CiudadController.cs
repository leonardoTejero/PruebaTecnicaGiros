using AppGiros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppGiros.Controllers
{
    public class CiudadController : Controller
    {
        public IConfiguration Configuration { get; set; }

        public CiudadController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Ajustar sacar logica del controlador
        [HttpGet]
        public IActionResult ObtenerCiudades()
        {
            List<CiudadesModels> listaCiudades = new List<CiudadesModels>();

            using (SqlConnection conexion = new SqlConnection(Configuration["ConnectionStrings:ConnectionStringSQLServer"]))
            {
                string query = "SELECT * FROM CIUDADES";
                conexion.Open();

                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // Leer los datos de cada registro y agregarlos a la lista de ciudades
                        CiudadesModels ciudad = new()
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            IdPais = reader.GetInt32(2),
                        };
                        listaCiudades.Add(ciudad);
                    }
                }

                conexion.Close();
            }

            return Ok(listaCiudades);
        }
     

        [HttpGet]
        public JsonResult ObtenerCiudadesPais(int idPais)
        {
            List<ConvertirJson> list = new List<ConvertirJson>();

            using (SqlConnection conexion = new SqlConnection(Configuration["ConnectionStrings:ConnectionStringSQLServer"]))
            {
                string query = "SELECT id_ciudad, nombre FROM CIUDADES WHERE id_pais = @idPais";
                conexion.Open();

                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@idPais", idPais); // Agregar el parámetro @idPais

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // Leer los datos de cada registro y agregarlos a la lista de ciudades
                        ConvertirJson item = new ConvertirJson
                        {
                            Value = reader.GetInt32(0),
                            Text = reader.GetString(1),
                        };

                        list.Add(item);
                    }
                }
            }
            return Json(list); // serializa la lista a JSON
        }

        [HttpPost]
        public IActionResult GuardarCiudad(CiudadesModels ciudad)
        {
            bool respuesta;
            try
            {
                using (SqlConnection conexion = new(Configuration["ConnectionStrings:ConnectionStringSQLServer"]))
                {
                    string query = "INSERT INTO CIUDADES VALUES(@nombre, @id_pais)";
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(query, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre", ciudad.Nombre);
                        command.Parameters.AddWithValue("@id_pais", ciudad.IdPais);
                        command.ExecuteNonQuery();
                    }
                    conexion.Close();
                }
                respuesta = true;
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta=false;
                return Ok(respuesta);
            }
        }

    }
}
