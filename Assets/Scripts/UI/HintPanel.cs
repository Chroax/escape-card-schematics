using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintPanel : MonoBehaviour
{
    public GameObject hintPanel;
    public GameObject cardHint;
    public TextMeshProUGUI cardHintText;
    public TextMeshProUGUI hintCost;

    public void PanelPopUp()
    {
        hintPanel.SetActive(true);
    }

    public void BackFromPanel()
    {
        hintPanel.SetActive(false);
    }

    public void CardHintShow()
    {
        cardHint.SetActive(true);
    }

    public void GetSetCardHintCost()
    {

    }

    public void GetSetCardHintText()
    {

    }
}
