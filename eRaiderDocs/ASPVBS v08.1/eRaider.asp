<%

  ' eRaider.asp v08.1
  ' Session variables providing information about authenticated user added

  function eRaiderLoginCheckKey(username, login_key)
    dim cmd
    dim parm
    dim rs
    dim result
    dim ip
    dim idnum

    ip = request.servervariables("REMOTE_ADDR")

    set cmd = server.createobject("ADODB.Command")
    cmd.activeconnection = session("eRaiderDBConnStr")
    cmd.commandtext = "eRaiderLoginCheckKey_v081"
    cmd.commandtype = adCmdStoredProc

    set parm = cmd.createparameter("username", adVarChar, adParamInput, 255, username)
    cmd.parameters.append parm

    set parm = cmd.createparameter("login_key", adVarChar, adParamInput, 255, login_key)
    cmd.parameters.append parm

    set parm = cmd.createparameter("ip", adVarChar, adParamInput, 255, ip)
    cmd.parameters.append parm

    set rs = cmd.execute

    result = rs("valid")

    if (result = 1) then
      session("eRaiderUsername") = username
      session("eRaiderFName") = rs("fname")
      session("eRaiderNName") = rs("nname")
      session("eRaiderMName") = rs("mname")
      session("eRaiderLName") = rs("lname")
      session("eRaiderJobTitle") = rs("jobtitle")
      session("eRaiderAddr1") = rs("addr1")
      session("eRaiderAddr2") = rs("addr2")
      session("eRaiderCity") = rs("city")
      session("eRaiderState") = rs("state")
      session("eRaiderZip") = rs("zip")
      session("eRaiderPhone") = rs("phone")
      session("eRaiderEmail") = rs("email")
      session("eRaiderIDNum") = rs("eRaiderID")
      session("eRaiderBannerID") = rs("bannerID")
    else
      session("eRaiderUsername") = ""
      session("eRaiderFName") = ""
      session("eRaiderNName") = ""
      session("eRaiderMName") = ""
      session("eRaiderLName") = ""
      session("eRaiderJobTitle") = ""
      session("eRaiderAddr1") = ""
      session("eRaiderAddr2") = ""
      session("eRaiderCity") = ""
      session("eRaiderState") = ""
      session("eRaiderZip") = ""
      session("eRaiderPhone") = ""
      session("eRaiderEmail") = ""
      session("eRaiderIDNum") = ""
      session("eRaiderBannerID") = ""
    end if

    rs.close

    set rs = nothing
    set cmd = nothing

    eRaiderLoginCheckKey = (result = 1)
  end function

  function eRaiderIsDispatchURL(url)
    dim url_len

    url_len = Len(url)
    if (ucase(url) = ucase(right(session("eRaiderDispatchURL"),url_len))) then
      eRaiderIsDispatchURL = true
    else
      eRaiderIsDispatchURL = false
    end if
  end function

  sub eRaiderShowSignoutButton
      response.write "<a href=""https://eraider.ttu.edu/signout.asp?redirect="+server.urlencode(session("eRaiderDispatchURL"))+""">"
      response.write "<img border=""0"" src=""http://eraider.ttu.edu/signout.gif""></a>"
  end sub

  ' In-line portion of code
  '----------------------------------------------------------------------------

  if (session("eRaiderELC") <> "") and (request.cookies("elc") <> "") and _
     (session("eRaiderELC") = request.cookies("elc"))  then
    ' Login context passed!  Good to go!  Fall through...
  else
    if ((request.querystring("elu") <> "") and (request.querystring("elk") <> "")) then
      if (eRaiderLoginCheckKey(request.querystring("elu"), request.querystring("elk"))) then
        session("eRaiderELC") = request.cookies("elc")
        session("logged_in") = 1
        ' Authentication passed!  Good to go!  Fall through...
      else
        if (session("eRaiderFailureURL") <> "") then
          response.redirect(session("eRaiderFailureURL"))
        else
          response.redirect("https://eraider.ttu.edu/signin.asp?redirect="+server.urlencode(session("eRaiderDispatchURL")))
        end if
      end if
    else
      if (Not eRaiderIsDispatchURL(request.servervariables("SCRIPT_NAME"))) then
        session("eRaiderBookmarkURL") = request.servervariables("SCRIPT_NAME")
        if (request.servervariables("QUERY_STRING") <> "") then
          session("eRaiderBookmarkURL") = session("eRaiderBookmarkURL") + "?" + request.servervariables("QUERY_STRING")
        end if
      else
        if (request.querystring <> "") then
          if (instr(1, session("eRaiderDispatchURL"), "?") = 0) then ' If we do not find a question mark...
            dispatchURLQueryString = "?" + request.querystring
          else
            dispatchURLQueryString = "&" + request.querystring
          end if
        else
          dispatchURLQueryString = ""
        end if

        session("eRaiderBookmarkURL") = ""
      end if
      response.redirect("https://eraider.ttu.edu/signin.asp?redirect="+server.urlencode(session("eRaiderDispatchURL")+dispatchURLQueryString))
    end if
  end if

  ' If the user went directly to a bookmarked page, handle that dispatch here.

  if (session("eRaiderBookmarkURL") <> "") then
    localBookmarkURL = session("eRaiderBookmarkURL")
    session("eRaiderBookmarkURL") = ""
    response.redirect(localBookmarkURL)
  end if
%>