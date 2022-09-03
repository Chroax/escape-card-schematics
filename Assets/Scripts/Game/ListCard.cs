using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListCard : MonoBehaviour
{
    public GameObject redList;
    public GameObject blueList;
    public GameObject yellowList;
    public GameObject greyList;
    public GameObject greenList;


    public void CloseAllListPanel()
    {
        redList.SetActive(false);
        blueList.SetActive(false);
        yellowList.SetActive(false);
        greyList.SetActive(false);
        greenList.SetActive(false);
    }

    public void OpenRedCard()
    {
        CloseAllListPanel();
        redList.SetActive(true);
    }

    public void OpenBlueCard()
    {
        CloseAllListPanel();
        blueList.SetActive(true);
    }

    public void OpenYellowCard()
    {
        CloseAllListPanel();
        yellowList.SetActive(true);
    }

    public void OpenGreyCard()
    {
        CloseAllListPanel();
        greyList.SetActive(true);
    }

    public void OpenGreenCard()
    {
        CloseAllListPanel();
        greenList.SetActive(true);
    }
}
