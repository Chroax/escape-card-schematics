using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineCardPanel : MonoBehaviour
{
    public GameObject silangButton;
    public void OpenPanelMachine()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }

    private void OnEnable(){
        GameManager.Instance.activePanel = ActivePanel.machine;
    }

    public void SelectCardChoice(){
        GameManager.Instance.listCardHolder.SetActive(true);
    }
    private void Update(){
        if(GameManager.Instance.selectedMachineCard != null){
            silangButton.SetActive(true);
        }
    }

    public void removeCardFromHolder()
    {
        silangButton.SetActive(false);
        GameManager.Instance.selectedMachineCard = null;
        GameManager.Instance.machineCardImageSelected.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
    }

    public void OnDisable()
    {
        this.removeCardFromHolder();
    }
}
