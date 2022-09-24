using System.Collections.Generic;

public static class DBManager 
{
    public static string team_name;
    public static string account_id;
    public static string player_id;
    public static int remaining_coins;
    public static float remaining_hours;
    public static int discardCardsCount;
    public static int scores;
    public static int mapID;
    public static List<string> ownedCards = new List<string>();
    public static bool isTutorial;
    public static bool firstLogin;
    public static bool isWin;
    public static bool LoggedIn => team_name != null;
    public static void Logout() => team_name = null;
}
