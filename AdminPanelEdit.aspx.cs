using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class AdminPanelEdit : System.Web.UI.Page
    {
        public int noofquestions;
        public ArrayList question = new ArrayList();
        public ArrayList option1 = new ArrayList();
        public ArrayList option2 = new ArrayList();
        public ArrayList option3 = new ArrayList();
        public ArrayList option4 = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn;
            SqlCommand cmd;
            string connstring = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            conn = new SqlConnection(connstring);
            using(conn)
            {
                String querystr = "SELECT * FROM quizinfo";
                cmd = new System.Data.SqlClient.SqlCommand(querystr, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        quizpin.Value = reader["quizpin"].ToString();
                        quiztitle.Value = reader["quiztitle"].ToString();
                        noquestions.Value = reader["noofquestions"].ToString();
                        key.Value = reader["answerkey"].ToString();
                        noofquestions = Int32.Parse(reader["noofquestions"].ToString());
                    }
                }
                else
                {
                    Response.Write("Sorry! No Information of Previous Quiz!");
                }
                reader.Close();
            }
            conn = new SqlConnection(connstring);
            using (conn)
            {
                String querystr = "SELECT * FROM questions";
                cmd = new System.Data.SqlClient.SqlCommand(querystr, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
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
                else
                {
                    Response.Write("Sorry! No Information of Previous Quiz!");
                }
                reader.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //MySql.Data.MySqlClient.MySqlConnection conn;
            //MySql.Data.MySqlClient.MySqlCommand cmd;
            System.Data.SqlClient.SqlConnection conn;
            System.Data.SqlClient.SqlCommand cmd;
            //request from the form
            String quizpin = Request.Form["quizpin"];
            String quiztitle = Request.Form["quiztitle"];
            quiztitle = quiztitle.Replace("'", "''");
            String key = Request.Form["key"];
            String noquestions = Request.Form["noquestions"];
            String[] question = Request.Form.GetValues("question");
            String[] option1 = Request.Form.GetValues("option1");
            String[] option2 = Request.Form.GetValues("option2");
            String[] option3 = Request.Form.GetValues("option3");
            String[] option4 = Request.Form.GetValues("option4");

            //insert into database
            string connstring = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            //String connstring = "Database=quiz;Password=facebook;Port=3306;Server=localhost;User=root";

            conn = new System.Data.SqlClient.SqlConnection(connstring);
            try
            {
                String querystr = "SELECT COUNT(*) FROM quizinfo";
                cmd = new System.Data.SqlClient.SqlCommand(querystr, conn);
                conn.Open();
                String result = cmd.ExecuteScalar().ToString();
                int result1 = Convert.ToInt32(result);
                if (result1 > 0)
                {
                    querystr = "Delete from quizinfo";
                    cmd = new System.Data.SqlClient.SqlCommand(querystr, conn);
                    cmd.ExecuteScalar();
                }

                querystr = "INSERT INTO quizinfo (quizpin, quiztitle, answerkey, noofquestions) VALUES('" + quizpin + "','" + quiztitle + "','" + key + "','" + noquestions + "')";
                cmd = new System.Data.SqlClient.SqlCommand(querystr, conn);
                //conn.Open();
                cmd.ExecuteNonQuery();
                //conn.Close();

                querystr = "SELECT COUNT(*) FROM questions";
                cmd = new System.Data.SqlClient.SqlCommand(querystr, conn);
                //conn.Open();
                result = cmd.ExecuteScalar().ToString();
                result1 = Convert.ToInt32(result);
                if (result1 > 0)
                {
                    querystr = "Delete from questions";
                    cmd = new System.Data.SqlClient.SqlCommand(querystr, conn);
                    cmd.ExecuteScalar();
                }
                for (int i = 0; i < question.Length; i++)
                {
                    querystr = "INSERT INTO questions (question, option1, option2, option3, option4) VALUES('" + question[i] + "','" + option1[i] + "','" + option2[i] + "','" + option3[i] + "','" + option4[i] + "')";
                    cmd = new System.Data.SqlClient.SqlCommand(querystr, conn);
                    //conn.Open();
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                Response.Redirect("AdminPanelEdit.aspx");
            }
            catch (Exception ex)
            {
                //Response.Write(ex);
            }
        }
    }
}