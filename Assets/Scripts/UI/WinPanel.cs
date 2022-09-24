using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WinPanel : MonoBehaviour
{
    
    private string uri = "https://schematics.its.ac.id/gameapi/finish.php";
    private string uriTotalTeam = "https://schematics.its.ac.id/gameapi/getall.php";
    private string sceneName;

    private void OnEnable()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().winSoundPlay();
        StartCoroutine(GetTotalWinTeam());
    }

    public void toMainMenu()
    {
        if (DBManager.isTutorial)
            GameManager.Instance.winPanel.SetActive(true);
        else
        {
            sceneName = "Free Scene";
            GameManager.Instance.ChangeScene(sceneName);
        }
        Debug.Log(DBManager.isTutorial);
    }
    IEnumerator PostWin()
    {
        Debug.Log(DBManager.scores);
        WWWForm form = new();

        form.AddField("team_name", DBManager.team_name);
        form.AddField("account_id", DBManager.account_id);
        form.AddField("player_id", DBManager.player_id);
        form.AddField("remaining_coins", DBManager.remaining_coins);
        form.AddField("remaining_hours", DBManager.remaining_hours.ToString());
        form.AddField("discardCardsCount", DBManager.discardCardsCount);
        form.AddField("scores", DBManager.scores);

        form.AddField("mapID", DBManager.mapID);
        if(DBManager.isWin)
            form.AddField("isWin", 1);
        else
            form.AddField("isWin", 0);
        string ownedCards = "";
        form.AddField("ownedCards", ownedCards);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
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
                
            }
        }
    }

    IEnumerator GetTotalWinTeam()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uriTotalTeam))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    if(!DBManager.isWin)
                    {
                        DBManager.remaining_coins = 0;
                        DBManager.isWin = true;
                        Debug.Log(webRequest.downloadHandler.text);
                        string[] total = webRequest.downloadHandler.text.Split('/');
                        DBManager.scores += 400 - (int.Parse(total[0]) * 5);
                        Debug.Log(DBManager.scores);
                        if (DBManager.scores < 200)
                            DBManager.scores = 200;
                    }
                    break;
            }
            Debug.Log(webRequest.result);
            StartCoroutine(PostWin())
                ;
        }
    }
}
