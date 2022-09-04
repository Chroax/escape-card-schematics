using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCardPanel : MonoBehaviour
{
    public GameObject[] listmap;
    private int index = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            changepanel();
        }
    }

    public void OpenPanelMap()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }
    public void changepanel()
    {
        listmap[index].SetActive(false);
        index++;
        if (index >= listmap.Length)
        {
            index = 0;
        }
        listmap[index].SetActive(true);
        //make switchcase for each map
        switch (index)
        {
            case 0:
                GameManager.Instance.activeMap = ActiveMap.Backyard;
                break;
            case 1:
                GameManager.Instance.activeMap = ActiveMap.BackyardandGardenShed;
                break;
            case 2:
                GameManager.Instance.activeMap = ActiveMap.LivingRoomandKitchen;
                break;
            case 3:
                GameManager.Instance.activeMap = ActiveMap.Basement;
            break;
            case 4:
                GameManager.Instance.activeMap = ActiveMap.IsolationRoom;
                break;
            case 5:
                GameManager.Instance.activeMap = ActiveMap.Hallway;
                break;
        }
    }
}
