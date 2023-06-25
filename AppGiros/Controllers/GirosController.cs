using AppGiros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using static AppGiros.Controllers.CiudadController;

namespace AppGiros.Controllers
{
    public class GirosController : Controller
    {

        public IConfiguration Configuration { get; set; }

        public GirosController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ObtenerGiros()
        {
            List<GirosModels> listaGiros = new List<GirosModels>();

            using (SqlConnection conexion = new SqlConnection(Configuration["ConnectionStrings:ConnectionStringSQLServer"]))
            {
                string query = "SELECT * FROM GIROS";
                conexion.Open();

                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // Leer los datos de cada registro y agregarlos a la lista de ciudades
                        GirosModels ciudad = new()
                        {
                            Id = reader.GetInt32(0),
                            Recibo = reader.GetString(1),
                            IdCiudad = reader.GetInt32(2),
                        };
                        listaGiros.Add(ciudad);
                    }
                }

                conexion.Close();
            }
            GirosViewModel viewModel = new GirosViewModel
            {
                ListaGiros = listaGiros
            };

            //return Ok(listaGiros);
            return View(viewModel);
        }


        [HttpGet]
        public JsonResult GetGiros(int idCiudad)
        {
            List<ConvertirJson> list = new List<ConvertirJson>();

            using (SqlConnection conexion = new SqlConnection(Configuration["ConnectionStrings:ConnectionStringSQLServer"]))
            {
                string query = "SELECT TOP 5 GIR_GIRO_ID, GIR_RECIBO FROM GIROS WHERE GIR_CIUDAD_ID = @idCiudad";
                conexion.Open();

                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    // Agregar el parámetro
                    command.Parameters.AddWithValue("@idCiudad", idCiudad); 

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

    }
}
