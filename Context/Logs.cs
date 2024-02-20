using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_status_liverpool.Context
{
    public class Logs: DBContext

    {
        public void Alta_logs(int id, string error, string aplicacion)
        {

            DateTime fecha = DateTime.Now;

            string connectionString = $"server ={GetRDSConections().Writer}; {Data_base}";

            //---------------------------------------------------------------------
            using (MySqlConnection cnn = new MySqlConnection(connectionString))
            {

                MySqlCommand cmd = new MySqlCommand("alta_logs_sp", cnn);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;




                cmd.Parameters.AddWithValue("logfecha", fecha);
                cmd.Parameters.AddWithValue("logerror", error);
                cmd.Parameters.AddWithValue("logaplicacion", aplicacion);
                cmd.Parameters.AddWithValue("usuid", null);

                cnn.Open();
                int res = cmd.ExecuteNonQuery();


                cmd = null;
                cnn.Close();

            }

        }//crea un registro en la tabla logs o errores 
    }
}