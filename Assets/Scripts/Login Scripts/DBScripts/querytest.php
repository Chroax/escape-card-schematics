<?php
session_start();
if (!isset($_SESSION["login"])) {
    echo "you are not logged in!";
    exit;
}
// some queries here
?>