using AppGiros.Manejadores;
using AppGiros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using static AppGiros.Controllers.CiudadController;

namespace AppGiros.Controllers
{
    public class GirosController : Controller
    {

        public IConfiguration Configuration { get; set; }
        public ManejadorGiros _manejadorGiros { get; set; }

        public GirosController(IConfiguration configuration, ManejadorGiros manejadorGiros)
        {
            Configuration = configuration;
            _manejadorGiros = manejadorGiros;
        }

        public IActionResult Index()
        {
            return View();
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
            return Json(list); 
        }

        [HttpGet]
        public JsonResult ObtenerGirosCiudad(int idCiudad)
        {
            var giros = _manejadorGiros.ObtenerGirosCiudad(idCiudad);
            return Json(giros);
        }

    }
}
