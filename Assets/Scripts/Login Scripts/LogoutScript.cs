using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LogoutScript : MonoBehaviour
{
    private string url = "https://schematics.its.ac.id/gameapi/logout.php";
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
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            webRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
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
                SceneManager.LoadScene("Scenes/Login Scene");
            }
        }
    }
}
