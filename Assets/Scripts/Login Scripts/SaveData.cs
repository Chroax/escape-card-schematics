using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SaveData : MonoBehaviour
{
    private string urlSave = "https://schematics.its.ac.id/gameapi/save.php";

    void Start()
    {
        if(!DBManager.isWin)
            StartCoroutine(PostData());
    }

    IEnumerator PostData()
    {
        while(!DBManager.isWin)
        {
            Debug.Log("Saving");
            WWWForm form = new();

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

            using (UnityWebRequest webRequest = UnityWebRequest.Post(urlSave, form))
            {
                webRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
                yield return webRequest.SendWebRequest();
                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(webRequest.error);
                }
                else
                {
                    webRequest.Dispose();
                    yield return new WaitForSeconds(2);
                }
            }
        }
    }
}
