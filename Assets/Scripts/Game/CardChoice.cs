using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardChoice : MonoBehaviour
{
    public CardDetailSO cardDetail;
    public void SelectCard()
    {
        if(GameManager.Instance.activePanel != ActivePanel.combine)
        {
            GameManager.Instance.panelChoiceCard.SetActive(false);

            switch (GameManager.Instance.activePanel)
            {
                case ActivePanel.hidden:
                    if(cardDetail.cardType != GameManager.Instance.hiddenCardType)
                    {
                        Debug.Log("Salah type card");
                        break;
                    }
                    GameManager.Instance.selectedCardHidden = cardDetail;
                    GameManager.Instance.hiddenCardImageSelected.GetComponent<Image>().sprite = cardDetail.cardSprite;
                    break;

                case ActivePanel.unlock:
                    if (cardDetail.cardType != GameManager.Instance.unlockCardType)
                    {
                        Debug.Log("Salah type card");
                        break;
                    }
                    GameManager.Instance.selectedCardUnlock = cardDetail;
                    GameManager.Instance.unlockCardImageSelected.GetComponent<Image>().sprite = cardDetail.cardSprite;
                    break;

                case ActivePanel.hint:
                    HintPanel.instance.GetSetCardHintText("");
                    GameManager.Instance.selectedCardHint = cardDetail;
                    GameManager.Instance.hintCardImageSelected.GetComponent<Image>().sprite = cardDetail.cardSprite;
                    break;

            }
        }
        else
        {
            
            if(GameManager.Instance.choiceCombineCard1 && !GameManager.Instance.choiceCombineCard2 && cardDetail.cardType == CardType.red)
            {
                GameManager.Instance.panelChoiceCard.SetActive(false);
                if (cardDetail.cardType != GameManager.Instance.combineCardType1)
                {
                    Debug.Log("Salah type card 1");
                    return;
                }
                GameManager.Instance.selectedCombineCard1 = cardDetail;

                GameManager.Instance.combineCardImageSelected1.GetComponent<Image>().sprite = cardDetail.cardSprite;
            }
            else if(!GameManager.Instance.choiceCombineCard1 && GameManager.Instance.choiceCombineCard2 && cardDetail.cardType == CardType.blue)
            {
                GameManager.Instance.panelChoiceCard.SetActive(false);
                if (cardDetail.cardType != GameManager.Instance.combineCardType2)
                {
                    Debug.Log("Salah type card 2");
                    return;
                }
                GameManager.Instance.selectedCombineCard2 = cardDetail;

                GameManager.Instance.combineCardImageSelected2.GetComponent<Image>().sprite = cardDetail.cardSprite;
            }else{
                Debug.Log("Warna Kartu Tidak Sesuai!");
                GameManager.Instance.panelChoiceCard.SetActive(false);
            }
        }
    }
}
