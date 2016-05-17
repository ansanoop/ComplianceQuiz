<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApplication3.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POLLTEK</title>
    <style type="text/css" media="screen">
        @import "iphonenav.css";
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="mxn.js?(googlev3)"></script>
    <script src="//code.jquery.com/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script type="application/x-javascript" src="iphonenav.js"></script>
    <script type="text/javascript" >
        function clickclear() {
            document.getElementById('message').value = '';

        }
        function clicksubmit(gowhere) {

            if (document.getElementById('name').value == "Your Name")
            { alert('Please input your name.'); }
            else
            {
                location.hash = gowhere;
                document.getElementById('message').value = '';
            }
        }
        function checkpin(gowhere) {
            var x = document.getElementById('pinid').value;
            //alert(x);
            if (document.getElementById('pin').value == x) {
                location.hash = gowhere;
                document.getElementById('message2').value = '';
            }
            else {
                alert('Wrong PIN.');
            }
        }

        selection = ' ';
        highestq = 0;
        function view(choice, qn) {
            selection = choice;
            //	if(qn==highestq || qn < highestq) //if they go back one or more questions 
            //		alert("Cheater");
            if (qn > highestq) //as long as the latest question answered is HIGHER than the last recieved submission, it will take.
            {
                //alert("Good. qn="+qn+" is higher than highestq="+highestq);
                highestq = qn;
                document.getElementById('message').value = document.getElementById('message').value + choice;	//append their answer choice to their string
                submitFormWithAjax();
            }

            //document.getElementById('message').value=document.getElementById('message').value + choice;	
            //submitFormWithAjax();	//submit no matter what

        }
        function submitFormWithAjax() {
            var t2lname = document.getElementById('name').value;
            var t2lmessage = document.getElementById('message').value;
           // alert("\nname"+t2lname+"\n"+t2lmessage);
            $.ajax({
                url: 'process.aspx/InsertMethod',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data:'{"name":"' + t2lname + '","message":"' + t2lmessage + '"}',
                //data: '{name: "' + t2lname + '" &message: "'+t2lmessage+'"}',
                //async: false,
                success: function (response) {
                    //$('[id*=txtTaxtypeName]').val('');
                    //alert("Record Has been Saved in Database");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) { //console.log('there is some error'); 
                    //alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                }
            });
        }


    </script>
</head>
<body>
    <h1 id="pageTitle"></h1>

    <ul id="home" title="PollTek 2014" selected="true">
        <li><a href="#security" id="qtitle" runat="server">Quiz</a></li>
        <li><a href="#q17" runat="server" id="aboutid"><font color="red">About</font></a></li>

    </ul>


    <ul id="security" class="panel" title="Quiz PIN?">
        <table bgcolor="white" cellpadding="0" width="20" cellspacing="0" style="margin-left: auto; margin-right: auto; text-align: center">
            <tr>
                <td>
                    <b>Please input the provided PIN.</b>
                    <img src="ttu.png" /><br/>
                    <input type='tel' name='pin' size="4" style="font-size: 20pt;" maxlength="4" value="0000" id='pin' onclick="this.value = '';" onfocus="this.select()" onblur="this.value=!this.value?'0000':this.value;" />
                    <input type='hidden' name='message2' id='message2' />
                    <input type="hidden" runat="server" id="pinid" value="" />
                    <br />
                    <b><a href="#" style="TEXT-DECORATION: NONE;" onclick="checkpin('#_page1');">Continue</a></b><br>
                    <br>
                </td>
            </tr>
        </table>
    </ul>
    <ul id="page1" class="panel" title="Name?">
        <table bgcolor="white" cellpadding="0" width="20" cellspacing="0" style="margin-left: auto; margin-right: auto; text-align: center">
            <tr>
                <td>
                    <b>Please input your name.</b>
                    <img src="ttu.png"><br/>
                    <input type='input' name='name' size="17" style="font-size: 16pt;" maxlength="17" value="Your Name" type="text" id='name' onclick="this.value = '';" onfocus="this.select()" onblur="this.value=!this.value?'Your Name':this.value;">
                    <input type='hidden' name='message' type="text" id='message'/>
                    <br/>
                    <b><a href="#" style="TEXT-DECORATION: NONE;" onclick="clicksubmit('#_q1');">Continue</a></b><br>
                    <br/>
                </td>
            </tr>
        </table>
    </ul>


    <ul id='ulaboutid' runat="server" class="panel" title="PollTek 2012">
        <table bgcolor="white" cellpadding="0" cellspacing="0" style="margin-left: auto; margin-right: auto; text-align: center">
            <tr>
                <td>
                    <img src="polltek.png" height='200' width='200'/>
                    <br/>
                    Designed by <b>Ravi Tek</b>.
                    <br/>
                    For TTU Athletics Compliance Use
                    <br/>
                    Copyright 2012.
                    <br/>
                    <a href="#home" onclick="name.value='Your Name';pin.value='0000';">Back</a>
                </td>
            </tr>
        </table>
    </ul>

    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
