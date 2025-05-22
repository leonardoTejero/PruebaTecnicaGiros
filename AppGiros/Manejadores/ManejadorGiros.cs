using AppGiros.Database;
using AppGiros.Models;
using System.Data.SqlClient;
using System.Data;

namespace AppGiros.Manejadores
{
    public class ManejadorGiros
    {
        private readonly DatabaseHelper _databaseHelper;
        public ManejadorGiros(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public bool CrearGiro(GirosModels giro)
        {
            string query = "INSERT INTO GIROS (GIR_RECIBO, GIR_CIUDAD_ID) VALUES (@Recibo, @IdCiudad)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Recibo", giro.Recibo),
                new SqlParameter("@IdCiudad", giro.IdCiudad)
            };

            return _databaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool EliminarGiro(int id)
        {
            string query = "DELETE FROM GIROS WHERE GIR_GIRO_ID = @Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };

            return _databaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool EditarGiro(GirosModels giro)
        {
            string query = "UPDATE GIROS SET GIR_RECIBO = @Recibo, GIR_CIUDAD_ID = @IdCiudad WHERE GIR_GIRO_ID = @Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Recibo", giro.Recibo),
                new SqlParameter("@IdCiudad", giro.IdCiudad),
                new SqlParameter("@Id", giro.Id)
            };

            // Ejecutar la consulta y devolver true si se actualizó al menos un registro
            return _databaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Obtiene los giros de una ciudad específica por id
        /// </summary>
        /// <param name="idPais"></param>
        /// <returns></returns>
        public List<GirosModels> ObtenerGirosCiudad(int idCiudad)
        {
            List<GirosModels> giros = new List<GirosModels>();

            string cantidadRegistros = "5";
            string query = $"SELECT TOP {cantidadRegistros} GIR_GIRO_ID, GIR_RECIBO FROM GIROS WHERE GIR_CIUDAD_ID = @idCiudad";
            DataTable tabla = _databaseHelper.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@idCiudad", idCiudad) });

            foreach (DataRow row in tabla.Rows)
            {
                giros.Add(new GirosModels { Id = (int)row["GIR_GIRO_ID"], Recibo = row["GIR_RECIBO"].ToString(), IdCiudad = idCiudad });
            }
            return giros;

        }
    }
}
