<?
	require('include.php');
	require('group1.php');
	require('group2.php');
	if(!in_array($_SESSION['eRaiderUsername'], $group1) && !in_array($_SESSION['eraiderUsername'], $group2)) {
		echo 'Not authorized.';
		exit;
	}
?>
<!-- Page content here -->
