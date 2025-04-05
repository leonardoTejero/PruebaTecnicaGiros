using AppGiros.Manejadores;
using AppGiros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace AppGiros.Controllers
{
    public class PaisesController : Controller
    {
        public IConfiguration Configuration { get; set; }
        public ManejadorPais _manejadorPais { get; set; }

        public PaisesController(IConfiguration configuration, ManejadorPais manejadorPais)
        {
            Configuration = configuration;
            _manejadorPais = manejadorPais;
        }

        public IActionResult Index()
        {
            List<SelectListItem> listaPaises = new List<SelectListItem>();

            using (SqlConnection conexion = new(Configuration["ConnectionStrings:ConnectionStringSQLServer"]))
            {
                string query = "SELECT * FROM PAISES";
                conexion.Open();

                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        // Leer los datos de cada registro y agregarlos a la lista de ciudades
                        SelectListItem item = new SelectListItem
                        {
                            Value = reader.GetInt32(0).ToString(),
                            Text = reader.GetString(1),
                        };

                        listaPaises.Add(item);

                    }
                }

                conexion.Close();
            }
            return View(listaPaises);
        }

        public IActionResult GuardarPais()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GuardarPais(PaisesModels pais)
         {
            using (SqlConnection conexion = new(Configuration["ConnectionStrings:ConnectionStringSQLServer"]))
            {
                using (SqlCommand cmd = new("sp_guardarPais", conexion))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("nombre", System.Data.SqlDbType.VarChar).Value = pais.Nombre;
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                }
            }
            return Redirect("Index");
        }

        public IActionResult EditarPais()
        {
            return View();
        }

        [HttpPut]
        public IActionResult EditarPais(PaisesModels pais)
        {
            using (SqlConnection conexion = new(Configuration["ConnectionStrings:ConnectionStringSQLServer"]))
            {

                // Crear la consulta SQL para actualizar el registro
                string query = "UPDATE PAIS SET Nombre = @Nombre WHERE Id = @Id";
                conexion.Open();

                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    // Pasar los parámetros a la consulta SQL
                    command.Parameters.AddWithValue("@Nombre", pais.Nombre);

                    // Ejecutar la consulta SQL
                    command.ExecuteNonQuery();
                }
            }

            // Redirigir a una página de éxito o mostrar un mensaje de éxito
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ObtenerPaises()
        {
            var paises = _manejadorPais.ObtenerPaises(); 
            return Json(paises);
        }
    }
}
