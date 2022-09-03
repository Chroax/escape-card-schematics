using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCardPanel : MonoBehaviour
{
    public void OpenPanelMap()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }
}
