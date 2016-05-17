Here's a zip file with some PHP code to help you get started.

Code from these files is maintained by Texas Tech's Application Development & Support department.
If you have questions, please contact them at 806-742-4500.

Before you can try it out, you'll need to register your application with TTUnet.  The site URL should be something like https://www.depts.ttu.edu/dept_name/application_name/index.php.  The IP address for www.depts.ttu.edu is 129.118.152.33.

Here’s an explanation of what each file does.
•eraider.php - This file handles interaction with the eRaider Sign-In system.  You shouldn't need to modify it. 
•include.php - When TTUnet approves your site, they will send you a database username and password.  You'll need to copy that information into this file, along with the site URL that you registered. 
•group1.php, group2.php - These files demonstrate a simple way to maintain groups of users. 
•index.php, group2only.php - These files show what to put at the top of your protected pages.  Members of either group can access the index.php page, but only members of group2 can access the group2only.php page. 
