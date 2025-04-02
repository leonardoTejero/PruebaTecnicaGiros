using AppGiros.Database;
using AppGiros.Models;
using System.Data;
using System.Data.SqlClient;

namespace AppGiros.Manejadores
{
    public class ManejadorCiudad
    {
        private readonly DatabaseHelper _databaseHelper;
        public ManejadorCiudad(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        /// <summary>
        /// Obtiene todas las ciudades de la base de datos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CiudadesModels> ObtenerCiudades()
        {
            var ciudades = new List<CiudadesModels>();
            try
            {
                using DataTable tabla = _databaseHelper.ExecuteQuery("SELECT id_ciudad, nombre, id_pais FROM CIUDADES");

                foreach (DataRow row in tabla.Rows)
                {
                    ciudades.Add(new CiudadesModels
                    {
                        Id = Convert.ToInt32(row["id_ciudad"]),
                        Nombre = row["nombre"]?.ToString() ?? string.Empty,
                        IdPais = Convert.ToInt32(row["id_pais"])
                    });
                }
            }
            catch (Exception ex)
            {
                // Loggear el error (usa un servicio de logs si tienes)
                Console.WriteLine($"Error al obtener ciudades: {ex.Message}");
                throw new Exception("Ocurrió un error al recuperar las ciudades.");
            }

            return ciudades;
        }

        /// <summary>
        /// Obtiene las ciudades de un país específico por id
        /// </summary>
        /// <param name="idPais"></param>
        /// <returns></returns>
        public List<CiudadesModels> ObtenerCiudadesPais(int idPais)
        {
            List<CiudadesModels> ciudades = new List<CiudadesModels>();

            string query = "SELECT id_ciudad, nombre FROM CIUDADES WHERE id_pais = @idPais";
            DataTable tabla = _databaseHelper.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@idPais", idPais) });

            foreach (DataRow row in tabla.Rows)
            {
                ciudades.Add(new CiudadesModels { Id = (int)row["id_ciudad"], Nombre = row["nombre"].ToString(), IdPais = idPais });
            }
            return ciudades;
            
        }

        /// <summary>
        /// Elimina una ciudad de la base de datos por su ID.
        /// </summary>
        /// <param name="idCiudad">ID de la ciudad a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False si no.</returns>
        public bool EliminarCiudad(int idCiudad)
        {
            string query = "DELETE FROM CIUDADES WHERE id_ciudad = @idCiudad";
            SqlParameter[] parametros = { new SqlParameter("@idCiudad", idCiudad) };

            return _databaseHelper.ExecuteNonQuery(query, parametros) > 0; 
        }

        public bool GuardarCiudad(CiudadesModels ciudad)
        {
            string query = "INSERT INTO CIUDADES VALUES(@nombre, @id_pais)";
            SqlParameter[] parametros =
            {
                new SqlParameter("@nombre", ciudad.Nombre),
                new SqlParameter("@id_Pais", ciudad.IdPais)
            };

            var result = _databaseHelper.ExecuteNonQuery(query, parametros) > 0;
            return result;
        }

    }
}
