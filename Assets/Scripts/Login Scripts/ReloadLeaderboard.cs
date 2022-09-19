using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class ReloadLeaderboard : MonoBehaviour
{
    private string uri = "";
    public void ClickButton()
    {
        StartCoroutine(LeaderboardRequest());
    }

    IEnumerator LeaderboardRequest()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    string[] rawResponse = webRequest.downloadHandler.text.Split('*');
                    foreach(string data in rawResponse)
                    {
                        if(data != "")
                        {
                            string[] info = data.Split("/");
                            Debug.Log("team_name: " + info[0] + ", scores: " + info[1]);
                        }   
                    }
                    break;
            }
        }
    }
}
