using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropCard : MonoBehaviour, IDropHandler
{
    [SerializeField] private bool isCombineCardLeft = false;
    public GameObject silangButton;
    
    public GameObject silangButtonLeft;
    public GameObject silangButtonRight;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Droped");
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        if (eventData.pointerDrag != null)
        {
            CardDetailSO cardDetail = eventData.pointerDrag.GetComponent<Card>().cardDetail;
            if (GameManager.Instance.activePanel != ActivePanel.combine)
            {
                switch (GameManager.Instance.activePanel)
                {
                    case ActivePanel.hidden:
                        silangButton.SetActive(true);
                        GameManager.Instance.selectedCardHidden = cardDetail;
                        GameManager.Instance.hiddenCardImageSelected.GetComponent<Image>().sprite = cardDetail.cardSprite;
                        GameManager.Instance.warningHidden.SetActive(false);
                        break;

                    case ActivePanel.unlock:
                        if (cardDetail.cardType != GameManager.Instance.unlockCardType)
                        {
                            Debug.Log("Salah type card");
                            break;
                        }
                        GameManager.Instance.warningHidden.SetActive(false);
                        silangButton.SetActive(true);
                        GameManager.Instance.selectedCardUnlock = cardDetail;
                        GameManager.Instance.unlockCardImageSelected.GetComponent<Image>().sprite = cardDetail.cardSprite;
                        break;

                    case ActivePanel.machine:
                        if (cardDetail.cardType != GameManager.Instance.machineCardType)
                        {
                            Debug.Log("Salah type card");
                            break;
                        }
                        silangButton.SetActive(true);
                        GameManager.Instance.selectedMachineCard = cardDetail;
                        GameManager.Instance.machineCardImageSelected.GetComponent<Image>().sprite = cardDetail.cardSprite;
                        GameManager.Instance.machineCardPanel.GetComponent<MachineCardPanel>().ActiveMachine();
                        break;


                }
            }
            else
            {
                if (isCombineCardLeft)
                {
                    Debug.Log("Droped kiri");
                    if (cardDetail.cardType != GameManager.Instance.combineCardType1)
                    {
                        Debug.Log("Salah type card 1");
                        return;
                    }
                    silangButtonLeft.SetActive(true);
                    GameManager.Instance.selectedCombineCard1 = cardDetail;
                    GameManager.Instance.combineCardImageSelectedRed.GetComponent<Image>().sprite = cardDetail.cardSprite;
                }
                else
                {
                    Debug.Log("Droped knani");
                    if (cardDetail.cardType != GameManager.Instance.combineCardType2)
                    {
                        Debug.Log("Salah type card 2");
                        return;
                    }
                    silangButtonRight.SetActive(true);
                    GameManager.Instance.selectedCombineCard2 = cardDetail;
                    GameManager.Instance.combineCardImageSelectedBlue.GetComponent<Image>().sprite = cardDetail.cardSprite;
                }
                GameManager.Instance.warningCombine.SetActive(false);
            }
        }
    }
}
