using AppGiros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using AppGiros.Manejadores;

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
        public IActionResult ObtenerCiudades()
        {
            try
            {
                var listaCiudades = _manejadorCiudad.ObtenerCiudades();
                return Ok(listaCiudades);
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
