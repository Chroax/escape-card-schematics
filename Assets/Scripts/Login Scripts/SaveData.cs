using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SaveData : MonoBehaviour
{
    public static string team_name;
    public static string account_id;
    public static string player_id;
    public static int remaining_coins;
    public static int remaining_hours;
    public static int discardCardsCount;
    public static int scores;
    public static int mapID;
    public static string ownedCards;
    [HideInInspector] private string urlSave = "http://localhost/gamedevDB/save.php";

    void Start()
    {
        StartCoroutine(PostData());
    }

    IEnumerator PostData()
    {
        while(true)
        {
            WWWForm form = new();
            Debug.Log(DBManager.team_name);
            Debug.Log(DBManager.account_id);
            Debug.Log(DBManager.player_id);
            Debug.Log(DBManager.remaining_coins);
            Debug.Log(DBManager.remaining_hours);
            Debug.Log(DBManager.discardCardsCount);
            Debug.Log(DBManager.scores);
            Debug.Log(DBManager.mapID);
            Debug.Log(DBManager.ownedCards);

            form.AddField("team_name", DBManager.team_name);
            form.AddField("account_id", DBManager.account_id);
            form.AddField("player_id", DBManager.player_id);
            form.AddField("remaining_coins", DBManager.remaining_coins);
            form.AddField("remaining_hours", DBManager.remaining_hours.ToString());
            form.AddField("discardCardsCount", DBManager.discardCardsCount);
            form.AddField("scores", DBManager.scores);
            form.AddField("mapID", DBManager.mapID);
            string ownedCards = "";
            for(int i = 0; i < DBManager.ownedCards.Count; i++)
            {
                ownedCards += DBManager.ownedCards[i];
                if (i < DBManager.ownedCards.Count - 1)
                    ownedCards += ",";
            }
            form.AddField("ownedCards", ownedCards);

            UnityWebRequest webRequest = UnityWebRequest.Post(urlSave, form);

            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log("server message " + webRequest.downloadHandler.text);
                //Check for a confirmation from the web

                webRequest.Dispose();
                yield return new WaitForSeconds(2);
            }
        }
    }
}
