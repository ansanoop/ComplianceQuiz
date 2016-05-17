<%@ Page Language="C#" %>

<!DOCTYPE html>
<script runat="server">
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
            querystr = "Delete from Shoutbox";
            cmd = new System.Data.SqlClient.SqlCommand(querystr, conn);
            cmd.ExecuteScalar();

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
        }
        catch (Exception ex)
        {
            //Response.Write(ex);
        }
    }
</script>



<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>Admin Panel</title>
    <script src="js/jquery-1.8.2.min.js"></script>
    <script src="js/jquery-ui-1.10.0.custom.min.js"></script>
    <script src="js/jquery.dropkick-1.0.0.js"></script>
    <script src="js/jquery.tagsinput.js"></script>
    <script src="js/bootstrap-tooltip.js"></script>
    <script src="js/jquery.placeholder.js"></script>
    <script src="video.js"></script>
    <script src="jquery-ui.js"></script>
    <!--[if lt IE 8]>
		  <script src="js/icon-font-ie7.js"></script>
		  <script src="js/icon-font-ie7-24.js"></script>
		<![endif]-->
    <style type="text/css">
        input:not([type]), input[type="text"] {
            font-size: 12px;
            font-weight: bold;
        }
    </style>
    <script>
        var count = 0;
        function addRowForm4() {
            $("#addRowHere").append("<table id='row" + count + "'><tr><td><font size='2px'><b>Question: </b></font></td><td style='width: 886px'><input type='text' value='NULL' style='margin-top:8px;margin:3px;font-size:12px; width: 876px;' name='question' ></td></tr><tr><td><font size='2px'><b>Option1: </b></font></td><td style='width: 886px'><input type='text' value='NULL' style='margin-top:8px;margin:3px;font-size:12px; width: 876px;' name='option1' ></td></tr><tr><td><font size='2px'><b>Option2: </b></font></td><td style='width: 886px'><input type='text' value='NULL' style='margin-top:8px;margin:3px;font-size:12px; width: 876px;' name='option2' ></td></tr><tr><td><font size='2px'><b>Option3: </b></font></td><td style='width: 886px'><input type='text' value='NULL' style='margin-top:8px;margin:3px;font-size:12px; width: 876px;' name='option3' ></td></tr><tr><td><font size='2px'><b>Option4: </b></font></td><td style='width: 886px'><input type='text' value='NULL' style='margin-top:8px;margin:3px;font-size:12px; width: 876px;' name='option4' ></td></tr><tr><td><img src='images/cross.png' onclick='$(\"#row" + count + "\").remove();' style='cursor: hand; cursor: pointer;'/><td></tr></table>");
            count++;
        }

    </script>
    <script>
        function isValidForm() {
            //alert("test");
            var isFormValid = true;

            $(".required").each(function () {
                if ($.trim($(this).val()).length == 0) {
                    $(this).addClass("highlight");
                    isFormValid = false;
                }
                else {
                    $(this).removeClass("highlight");
                }
            });

            if (!isFormValid) alert("Please fill in all the required fields (highlighted red)");
            if (isFormValid) {
                var quizpin = document.getElementById('quizpin').value;
                var nquest = document.getElementById('noquestions').value;
                if (isNaN(quizpin) || isNaN(nquest)) {
                    alert("enter number in quiz pin or number of questions");
                    isFormValid = false;
                }
            }
            return isFormValid;
        }
    </script>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href='css/layout.css' rel='stylesheet' type='text/css' />
    <link rel="shortcut icon" href="images/favicon.ico" />

</head>
<body>
    <div class="mainbody">
        <div class="title">
            <a href="index.asp">
                <img src="images/logo.gif" style="margin-top: 19px; margin-left: 19px;" /></a>
        </div>
        <br />
        <div class="content">
            <form id="form1" runat="server" method="post">
                <asp:Label ID="Label1" runat="server" Text="Quiz Pin:"></asp:Label>
                <!--<asp:TextBox ID="TextBox1" runat="server" name="quizpin" Width="70px" Height="10px" CssClass="required"></asp:TextBox>-->
                <input type="text" id="quizpin" name="quizpin" style="width: 70px; height: 10px;" class="required" />
                <asp:Label ID="Label2" runat="server" Text="Quiz Title"></asp:Label>
                <!--<asp:TextBox ID="TextBox2" runat="server" name="quiztitle" Width="225px" Height="10px" CssClass="required"></asp:TextBox>-->
                <input type="text" id="quiztitle" name="quiztitle" style="width: 225px; height: 10px;" class="required"/>
                <asp:Label ID="Label3" runat="server" Text="No of Questions"></asp:Label>
                <!--<asp:TextBox ID="TextBox3" runat="server" Width="33px" name="noquestions" Height="10px" CssClass="required"></asp:TextBox>-->
                <input type="text" id="noquestions" name="noquestions" style="width: 33px; height: 10px;" class="required"/>
                <asp:Label ID="Label4" runat="server" Text="Key"></asp:Label>
                <!--<asp:TextBox ID="TextBox4" runat="server" Height="10px" CssClass="required" name="key"></asp:TextBox>-->
                <input type="text" id="key" name="key" style="height: 10px;" class="required"/>

                <table id="addRowHere">
                </table>
                <a href="#" id="add" onclick="addRowForm4()">Click Here To Add a Question</a><br /><br />
                <a href="adminpaneledit.aspx" id="a1">Click Here To Edit Previous Quiz Info</a>
                <br />

                <asp:Button ID="Button1" runat="server" OnClientClick="return isValidForm()" Text="Save" OnClick="Button1_Click" />
            </form>
        </div>
    </div>
</body>
</html>
