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
            Debug.Log("bukan combine");
            GameManager.Instance.listCardHolder.SetActive(false);

            switch (GameManager.Instance.activePanel)
            {
                case ActivePanel.hidden:
                    GameManager.Instance.warningHidden.SetActive(false);
                    GameManager.Instance.selectedCardHidden = cardDetail;
                    GameManager.Instance.hiddenCardImageSelected.GetComponent<Image>().sprite = cardDetail.cardSprite;
                    break;

                case ActivePanel.unlock:
                    if (cardDetail.cardType != GameManager.Instance.unlockCardType)
                    {
                        Debug.Log("Salah type card");
                        break;
                    }
                    GameManager.Instance.warningUnlock.SetActive(false);
                    GameManager.Instance.selectedCardUnlock = cardDetail;
                    GameManager.Instance.unlockCardImageSelected.GetComponent<Image>().sprite = cardDetail.cardSprite;
                    break;

                case ActivePanel.hint:
                    HintPanel.instance.GetSetCardHintText("");
                    GameManager.Instance.selectedCardHint = cardDetail;
                    GameManager.Instance.hintCardImageSelected.GetComponent<Image>().sprite = cardDetail.cardSprite;
                    break;

                case ActivePanel.machine:
                    if (cardDetail.cardType != GameManager.Instance.machineCardType){
                        Debug.Log("Salah type card");
                        break;
                    }
                    GameManager.Instance.selectedMachineCard = cardDetail;
                    GameManager.Instance.machineCardImageSelected.GetComponent<Image>().sprite = cardDetail.cardSprite;
                    break;

            }
        }
        else
        {
            Debug.Log("MASUK KE COMBINE");
            
            if(GameManager.Instance.choiceCombineCard1 && !GameManager.Instance.choiceCombineCard2 && cardDetail.cardType == CardType.red)
            {
                GameManager.Instance.listCardHolder.SetActive(false);
                if (cardDetail.cardType != GameManager.Instance.combineCardType1)
                {
                    Debug.Log("Salah type card 1");
                    return;
                }
                GameManager.Instance.selectedCombineCard1 = cardDetail;

                GameManager.Instance.combineCardImageSelectedRed.GetComponent<Image>().sprite = cardDetail.cardSprite;
            }
            else if(!GameManager.Instance.choiceCombineCard1 && GameManager.Instance.choiceCombineCard2 && cardDetail.cardType == CardType.blue)
            {
                GameManager.Instance.listCardHolder.SetActive(false);
                if (cardDetail.cardType != GameManager.Instance.combineCardType2)
                {
                    Debug.Log("Salah type card 2");
                    return;
                }
                GameManager.Instance.selectedCombineCard2 = cardDetail;

                GameManager.Instance.combineCardImageSelectedBlue.GetComponent<Image>().sprite = cardDetail.cardSprite;
            }else{
                Debug.Log("Warna Kartu Tidak Sesuai!");
                GameManager.Instance.listCardHolder.SetActive(false);
            }
            GameManager.Instance.warningCombine.SetActive(false);
        }
    }
}
