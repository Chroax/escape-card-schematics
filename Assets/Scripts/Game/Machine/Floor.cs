using UnityEngine;
using TMPro;

public class Floor : MonoBehaviour
{
    public TMP_InputField inputX;
    public TMP_InputField inputY;
    public GameObject infoPanel;

    public void Reset()
    {
        inputX.text = "";
        inputY.text = "";
    }
    public void Submit()
    {
        if (inputX.text.Equals("36") && inputY.text.Equals("28"))
        {
            Debug.Log("Selamat jawaban benar");
        }
        else
        {
            Debug.Log("Jawaban mu salah");
            Reset();
        }
    }
}
