using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineCardPanel : MonoBehaviour
{
    public void OpenPanelMachine()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }
}
