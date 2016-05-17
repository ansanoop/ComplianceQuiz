<?
        session_start();
        $_SESSION['eRaiderDispatchURL'] = "https://www.depts.ttu.edu/dept_name/application_name/index.php";
        $_SESSION['eRaiderDBUsername'] = "ABC_XYZ";
        $_SESSION['eRaiderDBpassword'] = "***";
        // $_SESSION['eRaiderFailureURL'] = "<Optional URL goes here in the event of an authentication failure>";
        require('eraider.php');
?>
