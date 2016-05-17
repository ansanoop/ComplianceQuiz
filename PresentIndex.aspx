<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PresentIndex.aspx.cs" Inherits="WebApplication3.PresentIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>POLLTEK ADMIN PAGE</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    
    
    <script type="text/javascript">
        
    </script>
    <link rel="stylesheet" type="text/css" href="presentation.css" />
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
                url: 'PresentShout.aspx/InsertMethod',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{"n":"' + t2lname + '","m":"' + t2lmessage + '"}',
                success: function (response) {
                    //alert(response.d);
                    $("#shout").html(response.d);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //console.log('XMLHttpRequest' + XMLHttpRequest + "\n<br/>textStatus" + textStatus + "\n<br/>errorThrown" + errorThrown);
                }
            });
        }
        //setInterval("refresh_shoutbox()", 5000);
    </script>
</head><body>
            <div id="shout"></div>
</body>
</html>
