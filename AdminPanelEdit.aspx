<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPanelEdit.aspx.cs" Inherits="WebApplication3.AdminPanelEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Previous Quiz</title>
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
        var count = <%=noofquestions%>
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
            <form id="form1" runat="server">
                <asp:Label ID="Label1" runat="server" Text="Quiz Pin:"></asp:Label>
                <%--<asp:TextBox ID="TextBox1" runat="server" name="quizpin" Width="70px" Height="10px" CssClass="required"></asp:TextBox>--%>
                <input type="text" runat="server" id="quizpin" name="quizpin" style="width: 70px; height: 10px;" class="required" />
                <asp:Label ID="Label2" runat="server" Text="Quiz Title"></asp:Label>
                <%--<asp:TextBox ID="TextBox2" runat="server" name="quiztitle" Width="225px" Height="10px" CssClass="required"></asp:TextBox>--%>
                <input type="text" runat="server" id="quiztitle" name="quiztitle" style="width: 225px; height: 10px;" class="required" />
                <asp:Label ID="Label3" runat="server" Text="No of Questions"></asp:Label>
                <%--<asp:TextBox ID="TextBox3" runat="server" Width="33px" name="noquestions" Height="10px" CssClass="required"></asp:TextBox>--%>
                <input type="text" runat="server" id="noquestions" name="noquestions" style="width: 33px; height: 10px;" class="required" />
                <asp:Label ID="Label4" runat="server" Text="Key"></asp:Label>
                <%--<asp:TextBox ID="TextBox4" runat="server" Height="10px" CssClass="required" name="key"></asp:TextBox>--%>
                <input type="text" runat="server" id="key" name="key" style="height: 10px;" class="required" />
                    <%for (int i = 0; i < noofquestions; i++)
                      {%>
                    <table id="row<%=i %>">
                        <tr>
                            <td><font size='2px'><b>Question: </b></font></td>
                            <td style='width: 886px'>
                                <input type='text' value='<%=question[i] %>' style='margin-top: 8px; margin: 3px; font-size: 12px; width: 876px;' name='question'></td>
                        </tr>
                        <tr>
                            <td><font size='2px'><b>Option1: </b></font></td>
                            <td style='width: 886px'>
                                <input type='text' value='<%=option1[i] %>' style='margin-top: 8px; margin: 3px; font-size: 12px; width: 876px;' name='option1'></td>
                        </tr>
                        <tr>
                            <td><font size='2px'><b>Option2: </b></font></td>
                            <td style='width: 886px'>
                                <input type='text' value='<%=option2[i] %>' style='margin-top: 8px; margin: 3px; font-size: 12px; width: 876px;' name='option2'></td>
                        </tr>
                        <tr>
                            <td><font size='2px'><b>Option3: </b></font></td>
                            <td style='width: 886px'>
                                <input type='text' value='<%=option3[i] %>' style='margin-top: 8px; margin: 3px; font-size: 12px; width: 876px;' name='option3'></td>
                        </tr>
                        <tr>
                            <td><font size='2px'><b>Option4: </b></font></td>
                            <td style='width: 886px'>
                                <input type='text' value='<%=option4[i] %>' style='margin-top: 8px; margin: 3px; font-size: 12px; width: 876px;' name='option4'></td>
                        </tr>
                        <tr>
                            <td>
                                <img src='images/cross.png' onclick="$('#row<%=i %>').remove();" style='cursor: hand; cursor: pointer;' /><td>
                        </tr>
                    </table>
                    <%} %>
                <table id="addRowHere">
                </table>
                <a href="#" id="add" onclick="addRowForm4()">Click Here To Add a Question</a>
                <br />
                <asp:Button ID="Button1" runat="server" OnClientClick="return isValidForm()" Text="Save" OnClick="Button1_Click" />
            </form>
        </div>
    </div>
</body>
</html>
