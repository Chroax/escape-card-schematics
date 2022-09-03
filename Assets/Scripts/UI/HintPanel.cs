using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintPanel : MonoBehaviour
{
    public GameObject hintPanel;
    public GameObject cardHint;
    public GameObject confirmationPanel;
    public TextMeshProUGUI cardHintText;
    public TextMeshProUGUI hintCost;
    public Sprite placHolder;

    public void OnEnable()
    {
        GameManager.Instance.activePanel = ActivePanel.hint;
        hintPanel.SetActive(true);
    }

    public void OnDisable()
    {
        GameManager.Instance.selectedCardHint = null;
        GetSetCardHintText("");
        GameManager.Instance.hintCardImageSelected.GetComponent<Image>().sprite = placHolder;
        hintPanel.SetActive(false);
        GameManager.Instance.CloseAllPanel();
    }

    public void CardHintShow()
    {
        if(GameManager.Instance.selectedCardHint != null){
            confirmationPanel.SetActive(true);
            Debug.Log("OKE, OTW NGASIH HINT");
        }else{
            cardHint.SetActive(true);
            GetSetCardHintText("Tidak Ada Kartu!!!");
            Debug.Log("MANA ADA KARTU!!");
        }
    }
    public void SelectCardChoice()
    {
         GameManager.Instance.panelChoiceCard.SetActive(true);
    } 
    public void ConfirmHint()
    {
        confirmationPanel.SetActive(false);
        if(GameManager.Instance.player.UseCoin(5)){
            GetSetCardHintText(GameManager.Instance.selectedCardHint.cardDescription);
            Debug.Log("Coin Cukup");
        }else{
            GetSetCardHintText("Neleci Coin Tidak Cukup");
        }
        cardHint.SetActive(true);
    }
    
    public void DeclineHint()
    {
        confirmationPanel.SetActive(false);
    }
    
    public void GetSetCardHintCost()
    {

    }

    public void GetSetCardHintText(string text)
    {
        cardHintText.text = text;
    }
}
