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
    [SerializeField] private string url = "http://localhost/gamedevDB/login.php";

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
        form.AddField("name", usernameField.text);
        form.AddField("password", passwordField.text);

        // TL DR, make a new form and use the post method to send info

        UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
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
            if (webRequest.downloadHandler.text[0]=='0')
            {
                warningMessage.gameObject.SetActive(false);

                Debug.Log("Login confirmed!!");
                //Set a username and score to the system
                DBManager.username = usernameField.text;
            }
            else
            {
                //Clear username and password field to prep for another input
                warningMessage.text = $"wrong username or password!!";
                warningMessage.gameObject.SetActive(true);
            }

            //Check the state if user is logged in or not
            Debug.Log("Login " + DBManager.LoggedIn);

            if (DBManager.LoggedIn)
            {
                //Load a scene or something
                SceneManager.LoadScene("Main Menu");
            }

            webRequest.Dispose();
            yield return null;

        }
    }
    public void VerifyInput()
    {
        loginButton.interactable = (usernameField.text.Length >= 3 && passwordField.text.Length >= 3);
    }
}
