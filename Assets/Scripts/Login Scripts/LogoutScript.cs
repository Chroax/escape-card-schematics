using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LogoutScript : MonoBehaviour
{

    [SerializeField] private string url = "http://localhost/gamedevDB/logout.php";
    // Start is called before the first frame update
    public void CallLogout()
    {
        StartCoroutine(Logout());
    }
    IEnumerator Logout()
    {
        WWWForm form = new();
        form.AddField("request", DBManager.account_id);

        // TL DR, make a new form and use the post method to send info

        UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
        yield return webRequest.SendWebRequest();
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            DBManager.Logout();
            //Check the state if user is logged in or not
            webRequest.Dispose();
            yield return null;

        }
        if (!DBManager.LoggedIn)
        {
            //Load a scene or something
            SceneManager.LoadScene("Scenes/Login Scene");
        }


    }
}
