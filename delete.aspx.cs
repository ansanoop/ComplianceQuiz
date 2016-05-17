using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class delete : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlConnection conn;
        System.Data.SqlClient.SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString.Get("id");
            string connstring = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            conn = new System.Data.SqlClient.SqlConnection(connstring);
            try
            {
                string querystr = "Delete from shoutbox where name = '" + id + "'";
                cmd = new System.Data.SqlClient.SqlCommand(querystr, conn);
                conn.Open();
                cmd.ExecuteScalar();
                conn.Close();
                Response.Redirect("PanelIndex.aspx");
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }
    }
}