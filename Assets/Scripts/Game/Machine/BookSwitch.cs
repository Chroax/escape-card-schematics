using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookSwitch : MonoBehaviour
{
    public GameObject on;
    public GameObject off;
    public TextMeshProUGUI sign;
         
    public void OnAndOff()
    {
        if(on.activeInHierarchy)
        {
            sign.text = "OFF";
            on.SetActive(false);
            off.SetActive(true);
        }
        else
        {
            sign.text = "ON";
            off.SetActive(false);
            on.SetActive(true);
        }
    }
    public void Submit()
    {
        if(on.activeInHierarchy && !off.activeInHierarchy)
        {
            Debug.Log("Selamat anda berhasil masuk");
        }
        else
        {
            Debug.Log("Jawaban mu salah");
        }
    }
    public void ResetButton()
    {
        sign.text = "OFF";
        off.SetActive(true);
        on.SetActive(false);
    }
}
