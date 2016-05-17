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
    public partial class PresentShout : System.Web.UI.Page
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
            ArrayList question = new ArrayList();
            ArrayList option1 = new ArrayList();
            ArrayList option2 = new ArrayList();
            ArrayList option3 = new ArrayList();
            ArrayList option4 = new ArrayList();
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
            int[,] quest = new int[Int32.Parse(noofquestions) + 1, 5];
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
            int j = 0, savespot = 0;
            while (j < name.Count)
            {
                        for ( int participants = 0; participants < saved.Count; participants ++)
			                if(name[j].Equals(saved[participants]))
				                {rep=1; }
		                  saved.Add(name[j]);
                char[] msg = message[j].ToString().ToCharArray();
                try {
                    for (int counter = 0; counter < msg.Length; counter++)
                    {   //traverse through (Name) 14332323
                        int qnumber = counter + 1;
                        //echo '<br>Question ' .$qnumber. ' ' .$row['name']. ' answered ';
                        if (rep == 0)
                        {
                            if (msg[counter].Equals('1'))
                            { quest[qnumber, 1] = quest[qnumber, 1] + 1; }
                            else if (msg[counter].Equals('2'))
                            { quest[qnumber, 2] = quest[qnumber, 2] + 1; }
                            else if (msg[counter].Equals('3'))
                            { quest[qnumber, 3] = quest[qnumber, 3] + 1; }
                            else if (msg[counter].Equals('4'))
                            { quest[qnumber, 4] = quest[qnumber, 4] + 1; }
                        }
                    }
                }
                catch(Exception ex)
                {
                    //Console.Write(ex);
                }

		        savespot = savespot + 1;
		        rep=0;
                j = j + 1;
            }

            //LINK SCREEN
            string icon =  "<img src='ttu2.png' height='100' width='100'>";
            string br = "<br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br>";
		    output += "<table style='background-color:#000000' width='100%'><tr><td height='100' width='70'><a name='intro1'></a>"+ icon+"</td><td align='center'><font color='red' size=9>PollTek 2016: </font><font color='white' size=9>"+quiztitle+"</font></td><td align='right' width='110'>";
		    output += "<a href='#intro2'><img src='navright.png'  height='50' width='50'></a>";
		    output += "</td></font></td></tr></table>";
            output += "<table border='0' cellspacing='40' width='100%' align='center'><td align='center'><font size=9>On your mobile device, please navigate to:<br></font>";
            //output += "<font color='red' size=9><b>www.tinyurl.com/TTUComplianceQuiz<br></b></font><font color='red' size=9> or<br> </font>";
            output += "<font color='red' size=9><b>http://spark.ttu.edu/ComplianceQuiz<br></b></font><img src='qrcode.png' width='30%'></td></table>";
		    output += br;
            
            //PIN SCREEN
            output += "<table style='background-color:#000000' width='100%'><tr><td height='100' width='70'><a name='intro2'></a>"+icon+"</td><td align='center'><font color='red' size=9>PollTek 2016: </font><font color='white' size=9>"+quiztitle+"</font></td><td align='right' width='110'>";
		    output += "<a href='#intro1'><img src='navleft.png' height='50' width='50'></a>";
		    output += "<a href='#a1'><img src='navright.png'  height='50' width='50'></a>";
		    output += "</td></font></td></tr></table>";	
		    output += "<table border='0' cellspacing='40' width='100%' align='center'><td align='center'><font size=9>When prompted, input PIN:<br><font color='red' size=22>"+qpin+"</font><br><br><img src='qrcode.png' width='30%'></td></table>";
            output += br;


            //BEGIN THE QUESTION DATA LOOP!!
            int totalquestions = Int32.Parse(noofquestions);
	        for ( int graph = 1; graph <= totalquestions; graph++) 
	        {		
		        int prevquestion = graph - 1;
		        int nextquestion = graph + 1;		
		        output += "<!-- A SECTION OF QUESTION	--><table style='background-color:#000000' width='100%'><tr><td height='100' width='70'><a name='a"+graph+"'></a>"+icon+"</td><td align='center'><!-- CENTER OF TOP BAR --><font color='red' size=9>PollTek 2016: </font><font color='white' size=9>"+quiztitle+"</font></td><td align='right' width='110'>";
		        if(graph != 1)
		        output += "<a href='#a"+ prevquestion +"'><img src='navleft.png' height='50' width='50'></a>";
		        if(graph != totalquestions+1)
		        output += "<a href='#b"+graph+"'><img src='navright.png'  height='50' width='50'></a>";
		        output += "</td></font></td></tr></table>";
			
		        output += "<table border='0' cellspacing='40'>";
				output += "<tr><td>";
				output += "<font size=6><b>Q"+ graph +".<font color='red' size=6>"+question[graph-1]+"</font></b><br><br>";
				double apercent=0.0, bpercent=0.0, cpercent=0.0, dpercent=0.0, barwidth=800.0, barheight=40.0;
                int totalvotes = 0;
				totalvotes=quest[graph,1]+quest[graph,2]+quest[graph,3]+quest[graph,4];
				        if(quest[graph,1] != 0)
                            apercent = ((double)quest[graph, 1] / (double)totalvotes) * barwidth;
				        if(quest[graph,2] != 0)
                            bpercent = ((double)quest[graph, 2] / (double)totalvotes) * barwidth;
                        if(quest[graph,3] != 0)
                            cpercent = ((double)quest[graph, 3] / (double)totalvotes) * barwidth;
                        if(quest[graph,4] != 0)
                            dpercent = ((double)quest[graph, 4] / (double)totalvotes) * barwidth;
				
					    output += "<font color='black'>A. "+option1[graph-1]+"<br>";
					    output += "B. "+option2[graph-1]+"<br>";
					        if (!(option3[graph-1].Equals("NULL")))
					        output += "C. "+option3[graph-1]+"<br>";
					        if (!(option4[graph-1].Equals("NULL")))
					        output += "D. "+option4[graph-1]+"</font><br>";
					
				        totalvotes=quest[graph,1]+quest[graph,2]+quest[graph,3]+quest[graph,4];
				        output += "<br><br><br><font size=6 color='red'><b>"+totalvotes+"<font color='black' size=6> votes received.</font></b><br><br>";
				
		        //if($graph==$totalquestions)
		        output += "</table>";
		        output += br;
		
		
		        output += "<!-- B SECTION FOR QUESTION		-->";
		        output += "<table style='background-color:#000000' width='100%'><tr>";
		        output += "<td height='100' width='70'>";
		        output += "<a name='b"+graph+"'></a>"+icon+"</td>";
		        output += "<td align='center'>																								<!-- CENTER OF TOP BAR -->";
		        output += "<font color='red' size=9>PollTek 2016: </font><font color='white' size=9>"+quiztitle+"</font>	";
		        output += "</td><td align='right' width='110'>";
		        if(graph != 1)
		            output +=  "<a href='#a"+prevquestion+"'><img src='navleft.png' height='50' width='50'></a>";
		        if(graph != totalquestions)
		            output += "<a href='#a"+nextquestion+"'><img src='navright.png'  height='50' width='50'></a>";
		        output += "</td></font></td></tr></table>";
			
		        output +=  "<table border='0' cellspacing='40'>";
		        output +=  "<tr><td>";
		        output +=  "<font size=6><b>Q"+graph+".<font color='red' size=6>"+question[graph-1]+"</font></b><br><br>";
		        apercent=0.0;bpercent=0.0;cpercent=0.0;dpercent=0.0;
                totalvotes=0;
		        barwidth=800.0;
		        barheight=40.0;
		        totalvotes=quest[graph, 1]+quest[graph,2]+quest[graph, 3]+quest[graph,4];
		       if(quest[graph,1] != 0)
                   apercent = ((double)quest[graph, 1] / (double)totalvotes) * barwidth;
			    if(quest[graph,2] != 0)
                    bpercent = ((double)quest[graph, 2] / (double)totalvotes) * barwidth;
                if(quest[graph,3] != 0)
                    cpercent = ((double)quest[graph, 3] / (double)totalvotes) * barwidth;
                if(quest[graph,4] != 0)
                    dpercent = ((double)quest[graph, 4] / (double)totalvotes) * barwidth;
                
                char[] answerkey = key.ToCharArray();
		        if(Char.ToLower(answerkey[graph-1]).Equals('a')) //correct answer colorizer
			        output += "<font color='red'>"; 
                else 
                    output += "<font color='silver'>";
			    output += "A. "+option1[graph-1]+"<br>";
		        if(Char.ToLower(answerkey[graph-1]).Equals('b')) 
			        output += "<font color='red'>"; else output += "<font color='silver'>";			
			        output += "B. "+option2[graph-1]+"<br>";
		        if(!(option3[graph-1].Equals("NULL")))
			    {
				        if(Char.ToLower(answerkey[graph-1]).Equals('c')) 
			                output += "<font color='red'>"; else output += "<font color='silver'>";			
			                output += "C. "+option3[graph-1]+"<br>";
			    }
		        if(!(option4[graph-1].Equals("NULL")))
			    {
				        if(Char.ToLower(answerkey[graph-1]).Equals('d')) 
			                output += "<font color='red'>"; else output += "<font color='silver'>";			
			                output += "D. "+option4[graph-1]+"<br>";
			    }

		
		        //echo "<br><br>";

		        output += "<tr> <td style='vertical-align:middle'> ";
				output += "<font size=6>A <img src='statusbar.png' height='" +barheight+"' width='"+apercent+"'>"+quest[graph,1]+"</font><br>";
		        output += "<font size=6>B <img src='statusbar.png' height='" +barheight+"' width='"+bpercent+"'>"+quest[graph,2]+"</font><br>";
		        if(!(option3[graph-1].Equals("NULL")))
			        output += "<font size=6>C <img src='statusbar.png' height='" +barheight+"' width='"+cpercent+"'>"+quest[graph,3]+"</font><br>";
		        else
			        output += "</td></tr>";//kill the table if no C
		        if(!(option4[graph-1].Equals("NULL")))
			        output += "<font size=6>D <img src='statusbar.png' height='" +barheight+"' width='"+dpercent+"'>"+quest[graph,4]+"</font><br>";
		        else
			        output += "</td></tr>"; //kill the table if no D
			
	
		        //if($graph==$totalquestions)
			        output += "</table>";
		        output += br;
		
	        }

            return output;
        }
    }
}