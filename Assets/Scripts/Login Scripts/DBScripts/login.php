<?php

    include('connection.php');

    //  the next view variables are for the contents of the post method

    // clear sql like syntax to prevent hackerman

    $username=mysqli_real_escape_string($connect, $_POST["name"]);
    $usernameClean=filter_var($username, FILTER_SANITIZE_STRING, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);
    $password=$_POST["password"];
       
    $checkquery = "SELECT `team_name` , `password` FROM `accounts` WHERE `team_name`='".$usernameClean."';";
    $checkname = mysqli_query($connect, $checkquery) or die("name check failed");
    
    if (mysqli_num_rows($checkname)!=1) {
        
        echo "2\tuser with no name lah";
        exit;
    }
    
    $info = mysqli_fetch_assoc($checkname);
    $hashPass=md5($password);
    $hashDB=$info["password"];
    
    if ($hashPass != $hashDB) {
        echo "1\tFailure";
        exit;
    }
   
    // $_SESSION["login"]=true;
    echo "0\t". $info["team_name"];

?>
