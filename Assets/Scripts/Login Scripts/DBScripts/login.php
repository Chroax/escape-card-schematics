<?php

    include('connection.php');

    //  the next view variables are for the contents of the post method

    // clear sql like syntax to prevent hackerman

    $username=mysqli_real_escape_string($connect, $_POST["name"]);
    $usernameClean=filter_var($username, FILTER_SANITIZE_STRING, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);
    $password=$_POST["password"];
       
    $checkquery = "SELECT `team_name` , `password`, `first_login` FROM `accounts` WHERE `team_name`='".$usernameClean."';";
    $checkname = mysqli_query($connect, $checkquery) or die("name check failed");
    
    if (mysqli_num_rows($checkname)!=1) {
        
        echo "w\tuser with no name lah";
        exit;
    }
    
    $info = mysqli_fetch_assoc($checkname);
    $hashPass=md5($password);
    $hashDB=$info["password"];
    
    if ($hashPass != $hashDB) {
        echo "w\tFailure";
        exit;
    }
   
    $firstLogin=$info["first_login"];

    if ($firstLogin==1) {
        
        $logquery = 
        "INSERT INTO `account_log` (`account_log_id`, `account_id`) 
        SELECT team_name, account_id
        FROM accounts
        WHERE team_name='".$usernameClean."';";
        // echo $logquery;
        mysqli_query($connect, $logquery) or die("user log input failed");
        
        $updatefirst="UPDATE `accounts`
        SET first_login = 0
        WHERE team_name='".$usernameClean."';";
        mysqli_query($connect, $updatefirst) or die("first login update failed");
        
    }
    else {
        $logsecond="UPDATE `account_log`
        SET account_login = CURRENT_TIMESTAMP
        WHERE account_log_id='".$usernameClean."';";
        mysqli_query($connect, $logsecond) or die("login update failed");
    }

    
    // $_SESSION["login"]=true;
    echo "0\t". $info["team_name"];

?>
