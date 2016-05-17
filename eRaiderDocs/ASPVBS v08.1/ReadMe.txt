  '----------------------------------------------------------------------------
  '
  ' eRaider.asp v08.1
  '
  ' IMPORTANT NOTICE OF SOURCE DISTRIBUTION
  '
  ' This source code is provided by the Texas Tech University Telecommunications
  ' department to your organization for its sole use and is not to be
  ' re-distributed to any other organization or person.
  '
  ' The database account issued to your organization for eRaider web sign-in is 
  ' for the sole use of your organization.  Database account information
  ' (i.e., username, password) is not to be shared with any other organization or
  ' person.
  '_____________________________________________________________________________
  '
  ' eRaider.asp contains code needed to implement eRaider web sign-in functions.  It
  ' should be included at or near the top of any ASP page which is to be
  ' protected by eRaider web sign-in.
  '
  ' This code assumes that the web server has Microsoft Active Database Objects
  ' installed.  Also, the ADOVBS.INC file should be included in any file that is
  ' including this file.  Don't include it in this file or you will most likely
  ' run into a problem of circular includes.
  '
  ' You will need to define 2 session variables prior to the execution of this
  ' file.  It is recommended that you define the 2 session variables during the
  ' Session_OnStart event of this application within Global.asa.  However, you
  ' can define them in-line on each page prior to the inclusion of this file if
  ' you are not using a Global.asa file.  A basic Global.asa file is included
  ' with eRaider.asp for your convenience, but you will need to modify it to
  ' specify your application dispatch URL and database connection string.
  '
  ' Upon successful authentication, this code creates some session variables that
  ' can be used by the application programmer.
  '
  ' Created session variables:
  '
  '   Session("eRaiderUsername")
  '
  '     Upon successful authentication, this session variable will be created and
  '     contain the eRaider username of the authenticated user.
  '
  '   Session("eRaiderIDNum")
  '
  '     Upon successful authentication, this session variable will be created and
  '     contain the unique numeric identifier for the authenticated user.  The
  '     eRaiderIDNum is not a social security number and is not a Banner ID number.
  '     It is a unique number that is assigned to an eRaider account which never
  '     changes over the life of the account and is never reused by any other
  '     accounts.
  '
  '   Session("eRaiderBannerID")
  '
  '     Upon successful authentication, this session variable will be created and
  '     contain the Banner ID associated with this user.  Please be aware that not
  '     all eRaider account users have a matching identity within the Banner system.
  '     In these cases, the eRaiderBannerID session variable will be empty by design.
  '
  '   The following session variables are created with user-specific data:
  '
  '     Session("eRaiderFName")
  '     Session("eRaiderNName")
  '     Session("eRaiderMName")
  '     Session("eRaiderLName")
  '     Session("eRaiderJobTitle")
  '     Session("eRaiderAddr1")
  '     Session("eRaiderAddr2")
  '     Session("eRaiderCity")
  '     Session("eRaiderState")
  '     Session("eRaiderZip")
  '     Session("eRaiderPhone")
  '     Session("eRaiderEmail")
  '
  ' Required session variables:
  '
  '   Session("eRaiderDBConnStr")
  '
  '     This session variable specifies the connection string needed by your
  '     application to connect to the eRaider database.  Depending on your method
  '     of connection and your database library, your connection string may vary
  '     from other application writers.
  '
  '   Session("eRaiderDispatchURL")
  '
  '     This session variable specifies the URL registered with the eRaider
  '     web sign-in system for this application.
  '
  ' Optional session variables:
  '
  '   Session("eRaiderFailureURL")
  '
  '     This optional session variable specifies a URL that the user will be
  '     redirected to in the event the authentication fails.  If this session
  '     variable is not defined, then upon authenticaiton failure, the user's
  '     web browser would be redirected to the eRaider web sign-in site.
  '
  '----------------------------------------------------------------------------
  '
  ' Updates:
  '
  ' Date    |    Description                                           | Version
  '----------------------------------------------------------------------------
  '03/28/05 | Updated code to fix the problem of the query string not  |  1.1
  '         | being added onto the dispatch url and being passed to    |
  '         | eRaider sign-in.                                         |
  '----------------------------------------------------------------------------
  '03/30/05 | Added code to automatically create session variables     |  1.1
  '         | with user-specific data like FName, Addr1, Email, etc.   |
  '----------------------------------------------------------------------------
  '05/13/09 | Updated code to comply with new v08.1 calls (KBT)        | 08.1
  