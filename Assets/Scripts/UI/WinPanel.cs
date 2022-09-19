using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class WinPanel : MonoBehaviour
{
    private string uri = "http://localhost/gamedevDB/finish.php";
    private string uriTotalTeam = "http://localhost/gamedevDB/getall.php";

    private void OnEnable()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().winSoundPlay();
    }

    public void toMainMenu()
    {
        if(DBManager.isTutorial)
            SceneManager.LoadScene("Main Menu");
        else
        {
            StartCoroutine(GetTotalWinTeam());
            OpenSchematicsLink();
        }
    }
    public void OpenSchematicsLink()
    {
        #if !UNITY_EDITOR
        openWindow("https://schematics.its.ac.id/");
        #endif
    }

    IEnumerator PostWin()
    {
        WWWForm form = new();

        form.AddField("team_name", DBManager.team_name);
        form.AddField("account_id", DBManager.account_id);
        form.AddField("player_id", DBManager.player_id);
        form.AddField("remaining_coins", DBManager.remaining_coins);
        form.AddField("remaining_hours", DBManager.remaining_hours.ToString());
        form.AddField("discardCardsCount", DBManager.discardCardsCount);
        form.AddField("scores", DBManager.scores);
        form.AddField("mapID", DBManager.mapID);
        form.AddField("isWin", DBManager.isWin.ToString());
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
                yield return new WaitForSeconds(2);
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
                case UnityWebRequest.Result.Success:
                    string[] total = webRequest.downloadHandler.text.Split('/');
                    DBManager.scores = 1000 - int.Parse(total[0]) * 5;
                    if (DBManager.scores < 200)
                        DBManager.scores = 0;
                    break;
            }
        }
        StartCoroutine(PostWin());
    }

    [DllImport("_Internal")]
    private static extern void openWindow(string url);
}
