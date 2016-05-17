<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PanelIndex.aspx.cs" Inherits="WebApplication3.PanelIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POLLTEK ADMIN PAGE</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    
    <script src="//code.jquery.com/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        
    </script>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href='css/layout.css' rel='stylesheet' type='text/css' />
    <link rel="shortcut icon" href="images/favicon.ico" />
    <script type="text/javascript">
        $(function () {

            //populating shoutbox the first time
            refresh_shoutbox();
            // recurring refresh every 15 seconds
            setInterval("refresh_shoutbox()", 5000);

            $("#submit").click(function () {
                // getting the values that user typed
                
            });
        });

        function refresh_shoutbox() {
            var t2lname = "test";
            var t2lmessage = "trest";
            $.ajax({
                url: 'PanelShout.aspx/InsertMethod',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{"n":"' + t2lname + '","m":"' + t2lmessage + '"}',
                success: function (response) {
                    //alert(response.d);
                    $("#shout").html(response.d);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //console.log('there is some error'); 
                }
            });
      }
      //setInterval("refresh_shoutbox()", 5000);
    </script>
</head>
<body>
    <div class="mainbody">
        <div class="title">
            <a href="index.asp">
                <img src="images/logo.gif" style="margin-top: 19px; margin-left: 19px;" /></a>
        </div>
        <br />
            <div id="shout"></div>
            
       
    </div>
</body>
</html>
