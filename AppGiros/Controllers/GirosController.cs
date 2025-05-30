using AppGiros.Manejadores;
using AppGiros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

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

        [HttpPost]
        public IActionResult CrearGiro([FromBody] GirosModels giro)
        {
            if (giro == null || string.IsNullOrEmpty(giro.Recibo))
            {
                return BadRequest("Datos inválidos para crear el giro.");
            }

            try
            {
                _manejadorGiros.CrearGiro(giro);
                return Ok("Giro creado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el giro: {ex.Message}");
            }
        }

        [HttpPut]
        public IActionResult EditarGiro(GirosModels giro)
        {
            if (giro == null || giro.Id <= 0)
            {
                return BadRequest("Datos inválidos para editar el giro.");
            }

            try
            {
                _manejadorGiros.EditarGiro(giro);
                return Ok("Giro editado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar el giro: {ex.Message}");
            }
        }

        [HttpDelete]
        public IActionResult EliminarGiro(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido para eliminar el giro.");
            }

            try
            {
                _manejadorGiros.EliminarGiro(id);
                return Ok("Giro eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el giro: {ex.Message}");
            }
        }

    }
}
