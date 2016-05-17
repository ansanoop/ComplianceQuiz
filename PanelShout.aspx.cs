using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class PanelShout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat =
            System.Web.Script.Services.ResponseFormat.Json,
            UseHttpGet = false)]
        public static String InsertMethod(string n, string m)
        {
            string qpin = "", quiztitle = "", noofquestions = "", key = "", output = "";
            ArrayList datetime = new ArrayList();
            ArrayList name = new ArrayList();
            ArrayList message = new ArrayList();
            ArrayList saved = new ArrayList();
            saved.Add("");
            int rep = 0;
            System.Data.SqlClient.SqlConnection conn;
            System.Data.SqlClient.SqlCommand cmd;
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
                    key = reader["answerkey"].ToString();
                }
            }
            conn.Close();
            char[] anskey = key.ToCharArray();
            int[,] quest = new int[Int32.Parse(noofquestions) + 1, 5];
            output = "<h2>" + quiztitle + "</h2><br><span style='display:inline-block; width:387px;'>Answer key:</span><b><font size='4'>" + key + "</font></b><br>";
            querystr = "SELECT * FROM shoutbox order by datetime desc";
            cmd = new System.Data.SqlClient.SqlCommand(querystr, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    datetime.Add(reader["datetime"].ToString());
                    name.Add(reader["name"].ToString());
                    message.Add(reader["message"].ToString());
                }
            }
            conn.Close();
            int j = 0, q = 0, answeredtotal = 0, score = 0;
            double userscore = 0.0;
            while (j < name.Count)
            {
                for (int i = 0; i < saved.Count; i++)
                {
                    if (saved[i].Equals(name[j]))
                    {
                        rep = 1;
                        //output += "<b>repeat = 1</b>";
                    }
                }
                if (rep == 0)
                {
                    output += "<font size=4><a href='delete.aspx?id=" + name[j] + "'>(Delete)   </a>";
                    output += "<span style='display:inline-block; background-color:white; width:150px;font-weight:normal;'><font size=2>" + datetime[j] + "</font></span>";
                    output += "<b><span style='display:inline-block; background-color:white; width:190px;'>" + name[j] + "</span></b>";
                }
                saved.Add(name[j]);
                char[] msg = message[j].ToString().ToCharArray();
                //output += "<br/><b>test:"+msg[0]+"</b>";
                try {
                    for (int c = 0; c < msg.Length; c++)
                    {
                        q = c + 1;
                        if (rep == 0)
                        {
                            if (msg[c] == '1')
                            {
                                msg[c] = 'A';
                                quest[q, 1] += 1;
                                //output += "<br/><b>A test</b>";
                            }
                            if (msg[c] == '2')
                            {
                                msg[c] = 'B';
                                quest[q, 2] += 1;
                                //output += "<br/><b>B testdone</b>";
                            }
                            if (msg[c] == '3')
                            {
                                msg[c] = 'C';
                                quest[q, 3] += 1;
                                //output += "<br/><b>C testdone</b>";
                            }
                            if (msg[c] == '4')
                            {
                                msg[c] = 'D';
                                quest[q, 4] += 1;
                                //output += "<br/><b>D testdone</b>";
                            }
                            if ((char.ToLower(msg[c]).Equals(char.ToLower(anskey[c]))) && (c != Int32.Parse(noofquestions)))
                            {
                                output += "<font color='green'><b>" + msg[c] + "</b></font>";
                                score = score + 1;
                            }
                            else
                                output += "<font color='red'><b>" + msg[c] + "</b></font>";

                        }
                    }
                }
                catch(Exception ex)
                {
                    //Console.Write("error"+ex);
                }
                answeredtotal = q;
                //userscore = Convert.ToDouble((score / answeredtotal)*100);
                userscore = ((double)score / (double)answeredtotal) * 100.00;
                decimal d = (decimal)userscore;
                if (rep == 0)
                    output += "<font color='red'>     (" + Math.Round(d, 2) + "%) </font><br>";
                rep = 0;
                score = 0;

                j++;
            }
            output += "</font><br><br>";
            for (int graph = 1; graph <= Int32.Parse(noofquestions); graph++)
            {
                if (graph == 1)
                    output += "<table border='0' width='500'>";
                output += "<tr><td>";
                output += "<font size=2><b>Question " + graph + "</b></font> ";
                double apercent = 0.0, bpercent = 0.0, cpercent = 0.0, dpercent = 0.0, totalvotes = 0.0, barwidth = 400.0;
                totalvotes = quest[graph, 1] + quest[graph, 2] + quest[graph, 3] + quest[graph, 4];
                if (quest[graph, 1] != 0)
                    apercent = ((double)quest[graph, 1] / (double)totalvotes) * (double)barwidth;
                if (quest[graph, 2] != 0)
                    bpercent = ((double)quest[graph, 2] / (double)totalvotes) * (double)barwidth;
                if (quest[graph, 3] != 0)
                    cpercent = ((double)quest[graph, 3] / (double)totalvotes) * (double)barwidth;
                if (quest[graph, 4] != 0)
                    dpercent = ((double)quest[graph, 4] / (double)totalvotes) * (double)barwidth;

                int aper = Convert.ToInt32(apercent);
                output += "<tr> <td> A <img src='statusbar.png' style='width:"+apercent+"px;height:17px;'/>" + quest[graph, 1] + "</td></tr>";
                output += "<tr> <td> B <img src='statusbar.png' style='width:" + bpercent + "px;height:17px;'/>" + quest[graph, 2] + "</td></tr>";
                //if ($c[$graph] != "C") //new -- suppresses extraneous bars if the question doesnt feature c or d
                output += "<tr> <td> C <img src='statusbar.png' style='width:" + cpercent + "px;height:17px;'/>" + quest[graph, 3] + "</td></tr>";
                //if ($d[$graph] != "D") //new
                output += "<tr> <td> D <img src='statusbar.png' style='width:" + dpercent + "px;height:17px;'/>" + quest[graph, 4] + "</td></tr>";
                if (graph == Int32.Parse(noofquestions))
                    output += "</table>";

            }

            return output;
        }
    }
}