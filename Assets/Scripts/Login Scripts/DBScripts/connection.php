<?php

// configure as needed
include('DotEnv.php');

(new DotEnv(__DIR__ . '/.env'))->load();


$host=getenv('host');
$username = getenv('username');
$password = getenv('password');
$db=getenv('db');
$port=getenv('port');

// mysqli_connect(host, username, password, dbname, port, socket)

$connect= new mysqli($host, $username, $password, $db, $port);
if (mysqli_connect_errno()) {
    # code...
    echo "w\tConnection failed";
    exit();
}

?>