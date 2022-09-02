using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager 
{
    public static string username;
    public static string score;
    public static bool LoggedIn => username != null;
    public static void Logout() => username = null;

}
