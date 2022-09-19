using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    private string uri = "http://localhost/gamedevDB/finish.php";

    private void OnEnable()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().loseSoundPlay();
    }

    public void ToMainMenu()
    {
        if (DBManager.isTutorial)
            SceneManager.LoadScene("Main Menu");
        else
        {
            DBManager.isWin = false;
            StartCoroutine(PostLose());
            OpenSchematicsLink();
        }
    }

    IEnumerator PostLose()
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

    public void OpenSchematicsLink()
    {
        #if !UNITY_EDITOR
        openWindow("https://schematics.its.ac.id/");
        #endif
    }

    [DllImport("_Internal")]
    private static extern void openWindow(string url);
}
