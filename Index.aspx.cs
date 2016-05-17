using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Index : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlConnection conn;
        System.Data.SqlClient.SqlCommand cmd;
        ArrayList question = new ArrayList();
        ArrayList option1 = new ArrayList();
        ArrayList option2 = new ArrayList();
        ArrayList option3 = new ArrayList();
        ArrayList option4 = new ArrayList();
        String qpin, quiztitle, noofquestions;
        protected void Page_Load(object sender, EventArgs e)
        {
            //String connstring = "Database=quiz;Password=facebook;Port=3306;Server=localhost;User=root";
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            conn = new System.Data.SqlClient.SqlConnection(connstring);
            String querystr = "Select * from quizinfo";
            cmd = new System.Data.SqlClient.SqlCommand(querystr, conn);
            conn.Open();
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    qpin = reader["quizpin"].ToString();
                    quiztitle = reader["quiztitle"].ToString();
                    noofquestions = reader["noofquestions"].ToString();
                }
            }
            conn.Close();
            qtitle.InnerText = quiztitle;
            pinid.Value = qpin;
            int abid = Int32.Parse(noofquestions) + 1;
            aboutid.HRef = "#q"+abid;
            ulaboutid.ID = "q" + abid;
            querystr = "SELECT * FROM questions";
            cmd = new System.Data.SqlClient.SqlCommand(querystr, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    question.Add(reader["question"].ToString());
                    option1.Add(reader["option1"].ToString());
                    option2.Add(reader["option2"].ToString());
                    option3.Add(reader["option3"].ToString());
                    option4.Add(reader["option4"].ToString());
                }
            }
            conn.Close();
            for (int i = 0; i < question.Count; i++)
            {
                int nextq = i + 2;
                Response.Write("<ul id='q" + (i + 1) + "' title='Quiz PIN'");
                if (!(question[i].Equals("NULL")))
                    Response.Write("<li><font color='red'><b>" + question[i] + "</b></font></li>");
                if (!(option1[i].Equals("NULL")))
                    Response.Write("<li><a href='#q" + nextq + "' onclick='view(1," + i + 1 + ")'>" + option1[i] + "</a></li>");
                if (!(option2[i].Equals("NULL")))
                    Response.Write("<li><a href='#q" + nextq + "' onclick='view(2," + i + 1 + ")'>" + option2[i] + "</a></li>");
                if (!(option3[i].Equals("NULL")))
                    Response.Write("<li><a href='#q" + nextq + "' onclick='view(3," + i + 1 + ")'>" + option3[i] + "</a></li>");
                if (!(option4[i].Equals("NULL")))
                    Response.Write("<li><a href='#q" + nextq + "' onclick='view(4," + i + 1 + ")'>" + option4[i] + "</a></li>");
                Response.Write("</ul>");
            }
        }
    }
}