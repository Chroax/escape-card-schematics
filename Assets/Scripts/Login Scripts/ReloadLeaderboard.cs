using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;

public class ReloadLeaderboard : MonoBehaviour
{
    private List<GameObject> rankList = new List<GameObject>();
    public GameObject leaderboardContainer;
    public GameObject rankTemplate;
    public GameObject teamRank;
    private string uri = "https://schematics.its.ac.id/gameapi/leaderboard.php";
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
                            GameObject gameObject = null;
                            if (leaderboardContainer.transform.Find(info[0]) == null)
                            {
                                gameObject = Instantiate(rankTemplate, leaderboardContainer.transform);
                                gameObject.name = info[0];
                                rankList.Add(gameObject);
                            }
                            else
                                gameObject = leaderboardContainer.transform.Find(info[0]).gameObject;

                            gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = info[0];
                            gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = info[1];
                            if (DBManager.team_name == info[0])
                            {
                                teamRank.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = info[0];
                                teamRank.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = info[1];
                            }
                        }
                    }
                    if(rankList.Count > 0)
                    {
                        rankList.Sort(delegate (GameObject a, GameObject b)
                        {
                            return (int.Parse(a.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text).CompareTo
                            (int.Parse(b.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text)));
                        });
                        rankList.Reverse();
                    }

                    for (int i = 0; i < rankList.Count; i++)
                    {
                        rankList[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
                        rankList[i].transform.SetSiblingIndex(i);
                        if (DBManager.team_name == rankList[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text)
                            teamRank.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
                    }
                    break;
            }
        }
    }
}
