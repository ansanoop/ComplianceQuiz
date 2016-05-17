<?

  // eRaider.php v08.1

  session_start();

  function eRaiderLoginCheckKey($username, $login_key) {
      if(get_magic_quotes_gpc()) {
          $username = stripslashes($username);
          $login_key = stripslashes($login_key);
      }
      $username = str_replace("'","''",$username);
      $login_key = str_replace("'","''",$login_key);

      mssql_connect('eraiderDB.net.ttu.edu',$_SESSION['eRaiderDBUsername'],$_SESSION['eRaiderDBpassword']);
	  mssql_select_db('eRaider');
      $sql = "exec eRaiderLoginCheckKey_v081 @username='$username', @login_key='$login_key', @ip='".$_SERVER['REMOTE_ADDR']."'";
      $result = mssql_query($sql);
      $arr = mssql_fetch_array($result);
      mssql_close();
      if($arr['valid']=='1') {
        $_SESSION['eRaiderUsername'] = $username;
        $_SESSION['eRaiderFName'] = $arr['fname'];
        $_SESSION['eRaiderNName'] = $arr['nname'];
        $_SESSION['eRaiderMName'] = $arr['mname'];
        $_SESSION['eRaiderLName'] = $arr['lname'];
        $_SESSION['eRaiderJobTitle'] = $arr['jobtitle'];
        $_SESSION['eRaiderAddr1'] = $arr['addr1'];
        $_SESSION['eRaiderAddr2'] = $arr['addr2'];
        $_SESSION['eRaiderCity'] = $arr['city'];
        $_SESSION['eRaiderState'] = $arr['state'];
        $_SESSION['eRaiderZip'] = $arr['zip'];
        $_SESSION['eRaiderPhone'] = $arr['phone'];
        $_SESSION['eRaiderEmail'] = $arr['email'];
        $_SESSION['eRaiderIDNum'] = $arr['eRaiderID'];
        $_SESSION['eRaiderBannerID'] = $arr['bannerID'];
        return true;
      }
      else {
        $_SESSION['eRaiderUsername'] = '';
        $_SESSION['eRaiderFName'] = '';
        $_SESSION['eRaiderNName'] = '';
        $_SESSION['eRaiderMName'] = '';
        $_SESSION['eRaiderLName'] = '';
        $_SESSION['eRaiderJobTitle'] = '';
        $_SESSION['eRaiderAddr1'] = '';
        $_SESSION['eRaiderAddr2'] = '';
        $_SESSION['eRaiderCity'] = '';
        $_SESSION['eRaiderState'] = '';
        $_SESSION['eRaiderZip'] = '';
        $_SESSION['eRaiderPhone'] = '';
        $_SESSION['eRaiderEmail'] = '';
        $_SESSION['eRaiderIDNum'] = '';
        $_SESSION['eRaiderBannerID'] = '';
        return false;
      }
  }

  function eRaiderIsDispatchURL($url) {
    global $eRaiderDispatchURL;
    $url_len = strlen($url);
    $start_len = strlen($eRaiderDispatchURL) - $url_len;
    $nurl = substr($eRaiderDispatchURL, $start_len, $url_len);
    if ( strtoupper($url) == strtoupper($nurl) )
      return true;
    else
      return false;
  }

  // Display a logout button in compliance with the eRaider Sign-On Developer's Guide
  function eRaiderShowSignoutButton() {
      global $eRaiderDispatchURL;
      //echo '<a href="https://eraider.ttu.edu/signout.asp?redirect='.urlencode($eRaiderDispatchURL).'">';
      echo '<a href="https://eraider.ttu.edu/signout.asp">';
      echo '<img border="0" src="http://eraider.ttu.edu/signout.gif"></a>';
  }

  // In-line portion of code
  //----------------------------------------------------------------------------

  if (isset($_SESSION['eRaiderELC']) && isset($_COOKIE['elc']) && ($_SESSION['eRaiderELC'] == $_COOKIE['elc'])) {
    //Login context passed!  Good to go!  Fall through...
  }
  else {
    if (isset($_GET['elu']) && isset($_GET['elk'])) {
      if (eRaiderLoginCheckKey($_GET['elu'], $_GET['elk'])) {
        $_SESSION['eRaiderELC'] = $_COOKIE['elc'];
        // Authentication passed!  Good to go!  Fall through...
	if ($_SESSION['eRaiderBookmarkURL']<>'') {
          $localBookmarkURL = $_SESSION['eRaiderBookmarkURL'];
	  unset($_SESSION['eRaiderBookmarkURL']);
	  header('Location: ' . $localBookmarkURL);
	  exit;
	}
      }
      else {
        if (isset($_SESSION['eRaiderFailureURL'])) {
          header('Location: '.$_SESSION['eRaiderFailureURL']);
	  exit;
        }
        else {
          header('Location: https://eraider.ttu.edu/signin.asp?redirect='.urlencode($_SESSION['eRaiderDispatchURL']));
	  exit;
        }
      }
    }else {
      if (!eRaiderIsDispatchURL($_SERVER['SCRIPT_NAME'])) {
        $_SESSION['eRaiderBookmarkURL'] = $_SERVER['SCRIPT_NAME'];
        if (isset($_SERVER['QUERY_STRING'])) {
          $_SESSION['eRaiderBookmarkURL'] = $_SESSION['eRaiderBookmarkURL'] . '?' . $_SERVER['QUERY_STRING'];
        }
      }
      header('Location: https://eraider.ttu.edu/signin.asp?redirect='.urlencode($_SESSION['eRaiderDispatchURL']));
      exit;
    }
  }

?>
