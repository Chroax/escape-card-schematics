using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
public class LoginScript : MonoBehaviour
{
    
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private Button loginButton;
    [SerializeField] private TextMeshProUGUI warningMessage;
    [HideInInspector] private string urlLogin = "http://localhost/gamedevDB/login.php";

    private void Start()
    {
        warningMessage.gameObject.SetActive(false);
    }

    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm form = new();
        form.AddField("team_name", usernameField.text);
        form.AddField("password", passwordField.text);

        // TL DR, make a new form and use the post method to send info

        UnityWebRequest webRequest = UnityWebRequest.Post(urlLogin, form);
        yield return webRequest.SendWebRequest(); 
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            //Check for a confirmation from the web
            if (webRequest.downloadHandler.text[0]=='0')
            {
                string rawResponse = webRequest.downloadHandler.text;
                string[] users = rawResponse.Split('/');
                DBManager.team_name = users[1];
                DBManager.account_id = users[2];
                DBManager.player_id = users[3];
                int.TryParse(users[4], out DBManager.remaining_coins);
                float.TryParse(users[5], out DBManager.remaining_hours);
                int.TryParse(users[6], out DBManager.discardCardsCount);
                int.TryParse(users[7], out DBManager.scores);
                int.TryParse(users[8], out DBManager.mapID);
                DBManager.ownedCards.RemoveRange(0, DBManager.ownedCards.Count);
                string[] cards = users[9].Split(",");
                foreach (string card in cards)
                    DBManager.ownedCards.Add(card);
                int diffTime;
                int.TryParse(users[10], out diffTime);
                DBManager.remaining_hours -= diffTime;
                if (users[11] == "true")
                {
                    DBManager.firstLogin = true;
                }
                else
                {
                    DBManager.firstLogin = false;
                }
                if (users[12] == "1")
                    DBManager.isTutorial = true;
                else
                    DBManager.isTutorial = false;
            }
            else
            {
                //Clear username and password field to prep for another input
                warningMessage.text = $"wrong username or password!!";
                warningMessage.gameObject.SetActive(true);
            }

            //Check the state if user is logged in or not

            if (DBManager.LoggedIn)
            {
                Debug.Log("Berhasil Login");
                SceneManager.LoadScene("Main Menu");
            }
            webRequest.Dispose();
            yield return null;
        }
    }
}
