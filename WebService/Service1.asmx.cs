using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data.SqlClient;


namespace WebService
{
    /// <summary>
    /// Descripción breve de Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        private static string GetConnectionString()
        {
            string dtsConn = "Data Source=.\\SQLEXPRESS;AttachDbFilename=\"D:\\FDA\\Test Tecnicos - para ingresos laborales\\Practicos\\TestML2016\\bd\\bdtestfda.mdf\";Integrated Security=True;Connect Timeout=30;User Instance=True";
            return dtsConn;
        }

        [WebMethod]
        public string clima(string day)
        {
            string res = string.Empty;
            int dayP;
            if (int.TryParse(day, out dayP))
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand comm = new SqlCommand("dbo.fn_GetWeatherDayWS", conn))
                    {
                        comm.CommandType = CommandType.StoredProcedure;

                        SqlParameter p1 = new SqlParameter("@DayP", SqlDbType.Int);
                        SqlParameter p2 = new SqlParameter("@Weather", SqlDbType.VarChar, 50);

                        p1.Direction = ParameterDirection.Input;
                        p2.Direction = ParameterDirection.ReturnValue;

                        p1.Value = dayP;

                        comm.Parameters.Add(p1);
                        comm.Parameters.Add(p2);

                        conn.Open();
                        comm.ExecuteNonQuery();

                        if (p2.Value != DBNull.Value)
                            res = (string)p2.Value;
                        else
                            res = "Sin datos";
                    }
                }
            }
            return res;
        }



    }
}
