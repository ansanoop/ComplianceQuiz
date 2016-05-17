using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class process : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat =
            System.Web.Script.Services.ResponseFormat.Json,
            UseHttpGet = false)]
        public static bool InsertMethod(string name, string message)
        {
            DateTime localDate = DateTime.Now;
            //System.Diagnostics.Debug.Write("\nname:" + name+"\nmessage"+message);
            //Console.WriteLine("\nname:" + name + "\nmessage:" + message);
            //Console.ReadLine();
            System.Data.SqlClient.SqlConnection conn;
            System.Data.SqlClient.SqlCommand cmd;
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            conn = new System.Data.SqlClient.SqlConnection(connstring);
            string querystr = "INSERT INTO shoutbox (datetime,name,message) VALUES('"+localDate +"','" + name + "','" + message + "')";
            cmd = new System.Data.SqlClient.SqlCommand(querystr, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }
    }
}