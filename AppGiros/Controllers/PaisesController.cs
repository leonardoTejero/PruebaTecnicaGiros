using AppGiros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppGiros.Controllers
{
    public class PaisesController : Controller
    {
        public IConfiguration Configuration { get; set; }

        public PaisesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            //List<PaisesModels> listaPaises = new List<PaisesModels>(); 
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


//        using (SqlConnection conexion = new (Configuration["ConnectionStrings:ConnectionStringSQLServer"]))
//            {
//                using (SqlCommand cmd = new ("sp_obtenerPaises", conexion))
//                {
//                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
//                    conexion.Open();
//                    SqlDataAdapter adapter = new(cmd);
//    DataTable dataTable = new DataTable();
//    adapter.Fill(dataTable);
//                    dataTable.Dispose();

//                    List<PaisesModels> listaPaises = new();

//                    for (int i = 0; i<dataTable.Rows.Count; i++)
//                    {
//                        listaPaises.Add(new PaisesModels()
//    {
//        IdPais = Convert.ToInt32(dataTable.Rows[i][0]),
//                            Nombre = (dataTable.Rows[i][1]).ToString()

//                        });
//                    }
//ViewBag.listaPaises = listaPaises;
//conexion.Close();

//                }
//                return View();
//            }


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
        public IActionResult obtenerPaises()
        {
            List<PaisesModels> listaPaises = new();
            using (SqlConnection conexion = new(Configuration["ConnectionStrings:ConnectionStringSQLServer"]))
            {
                using (SqlCommand cmd = new("sp_obtenerPaises", conexion))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conexion.Open();
                    SqlDataAdapter adapter = new(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataTable.Dispose();

                    //List<PaisesModels> listaPaises = new();

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        listaPaises.Add(new PaisesModels()
                        {
                            IdPais = Convert.ToInt32(dataTable.Rows[i][0]),
                            Nombre = (dataTable.Rows[i][1]).ToString()

                        });
                    }
                    ViewBag.listaPaises = listaPaises;
                    conexion.Close();

                }
                return Ok(listaPaises);
            }
        }

        [HttpGet]
        public JsonResult GetPaises(int idPais)
        {
            List<ConvertirJson> list = new List<ConvertirJson>();

            using (SqlConnection conexion = new(Configuration["ConnectionStrings:ConnectionStringSQLServer"]))
            {

                string query = "SELECT id_Ciudad, nombre FROM CIUDAD WHERE id_Pais = @id_Pais";
                SqlParameter parameter = new SqlParameter("@id_Pais", idPais);
                conexion.Open();

                using (SqlCommand command = new SqlCommand(query, conexion))
                {
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
            return Json(list); // serializa la lista a json 
        }

        //clase modelo para enviar datos, llenar combo box
        public class ConvertirJson
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }
    }
}
