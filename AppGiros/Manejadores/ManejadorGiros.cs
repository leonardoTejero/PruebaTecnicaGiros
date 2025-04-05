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
