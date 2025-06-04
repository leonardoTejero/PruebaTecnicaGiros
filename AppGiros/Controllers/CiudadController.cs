using AppGiros.Manejadores;
using AppGiros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace AppGiros.Controllers
{
    public class CiudadController : Controller
    {
        public IConfiguration Configuration { get; set; }
        public ManejadorCiudad _manejadorCiudad { get; set; }

        public CiudadController(IConfiguration configuration, ManejadorCiudad manejadorCiudad) 
        {
            Configuration = configuration;
            _manejadorCiudad = manejadorCiudad;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ObtenerCiudades(int page = 1, int pageSize = 15)
        {
            try
            {
                // restriccion de resultados para paginacion
                var listaCiudades = _manejadorCiudad.ObtenerCiudades()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

                var totalCiudades = _manejadorCiudad.ObtenerCiudades().Count();

                return Ok(new { ciudades = listaCiudades, totalCiudades });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult EliminarCiudad(int idCiudad)
        {
            try
            {
                bool eliminada = _manejadorCiudad.EliminarCiudad(idCiudad);
                return Ok(eliminada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public JsonResult ObtenerCiudadesPais(int idPais)
        {
            var ciudadesPais = _manejadorCiudad.ObtenerCiudadesPais(idPais);
            return Json(ciudadesPais);
        }

        [HttpPost]
        public IActionResult GuardarCiudad(CiudadesModels ciudad)
        {
            bool respuesta;
            try
            {
                respuesta = _manejadorCiudad.GuardarCiudad(ciudad); 
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
