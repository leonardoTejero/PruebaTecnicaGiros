using AppGiros.Database;
using AppGiros.Models;
using System.Data;

namespace AppGiros.Manejadores
{
    public class ManejadorPais
    {
        private readonly DatabaseHelper _databaseHelper;
        public ManejadorPais(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        /// <summary>
        /// Obtiene todas los paises de la base de datos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PaisesModels> ObtenerPaises()
        {
            var paises = new List<PaisesModels>();
            try
            {
                using DataTable tabla = _databaseHelper.ExecuteQuery("SELECT * FROM PAISES");

                foreach (DataRow row in tabla.Rows)
                {
                    paises.Add(new PaisesModels
                    {
                        IdPais = Convert.ToInt32(row["id_pais"]),
                        Nombre = row["nombre"]?.ToString() ?? string.Empty,
                    });
                }
            }
            catch (Exception ex)
            {
                // Loggear el error (usa un servicio de logs si tienes)
                Console.WriteLine($"Error al obtener ciudades: {ex.Message}");
                throw new Exception("Ocurrió un error al recuperar los paises.");
            }
            return paises;
        }

    }
}
