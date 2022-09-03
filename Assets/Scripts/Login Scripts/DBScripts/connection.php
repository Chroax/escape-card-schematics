<?php

// configure as needed

$server="localhost";
$username = "root";
$password = "";
$db="gameschematics";

$connect= new mysqli($server, $username, $password, $db);
if (mysqli_connect_errno()) {
    # code...
    echo "Connection failed";
    exit();
}

?>