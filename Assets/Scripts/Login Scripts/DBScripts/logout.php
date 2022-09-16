<?php
include('connection.php');
$username= $_POST["request"];
$logoutlog = "UPDATE `account_log`
SET account_logout = CURRENT_TIMESTAMP
WHERE account_log_id='".$username."';";
mysqli_query($connect, $logoutlog) or die("log out log failure");
echo $username;

?>