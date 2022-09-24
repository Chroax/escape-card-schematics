using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineCardPanel : MonoBehaviour
{
    public GameObject silangButton;
    public GameObject MachinePlaceholder;
    public GameObject ElectricityMachine;
    public GameObject MysteriousMapMachine;
    public GameObject BookSwitchMachine;
    public GameObject FloorMachine;
    public GameObject PotionMakerMachine;
    public void OpenPanelMachine()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }

    private void OnEnable(){
        MachinePlaceholder.SetActive(true);
        GameManager.Instance.activePanel = ActivePanel.machine;
    }

    public void SelectCardChoice(){
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        GameManager.Instance.listCardHolder.SetActive(true);
    }
    private void Update(){
        if(GameManager.Instance.selectedMachineCard != null){
            silangButton.SetActive(true);
        }
    }

    public void RemoveCardFromHolder()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        silangButton.SetActive(false);
        GameManager.Instance.selectedMachineCard = null;
        GameManager.Instance.machineCardImageSelected.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
        DeactivateMachine();
        MachinePlaceholder.SetActive(true);
    }

    public void OnDisable()
    {
        if (GameManager.Instance.selectedMachineCard != null)
            RemoveCardFromHolder();
    }

    public void ActiveMachine()
    {
        DeactivateMachine();
        switch (GameManager.Instance.selectedMachineCard.cardID)
        {
            case "71":
                ElectricityMachine.SetActive(true);
                break;
            case "Y":
                MysteriousMapMachine.SetActive(true);
                break;
            case "3":
                BookSwitchMachine.SetActive(true);
                break;
            case "68":
                FloorMachine.SetActive(true);
                break;
            case "28":
                PotionMakerMachine.SetActive(true);
                break;
        }
    }

    public void DeactivateMachine()
    {
        MachinePlaceholder.SetActive(false);
        ElectricityMachine.SetActive(false);
        MysteriousMapMachine.SetActive(false);
        BookSwitchMachine.SetActive(false);
        FloorMachine.SetActive(false);
        PotionMakerMachine.SetActive(false);
    }
}
