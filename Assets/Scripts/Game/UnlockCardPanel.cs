using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockCardPanel : MonoBehaviour
{
    public TMP_InputField inputText;
    public GameObject warning;
    public GameObject PenaltyPanel;
    public GameObject silangButton;

    private void OnEnable()
    {
        GameManager.Instance.activePanel = ActivePanel.unlock;
    }

    public void removeCardFromHolder()
    {
        silangButton.SetActive(false);
        GameManager.Instance.selectedCardUnlock = null;
        GameManager.Instance.unlockCardImageSelected.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
    }

    public void UnlockCardSubmit()
    {
        if (GameManager.Instance.selectedCardUnlock == null)
        {
            warning.SetActive(true);
        }
        else if (GameManager.Instance.selectedCardUnlock.unlockCardAnswer == inputText.text && inputText.text != "0" && inputText.text != "")
        {
            warning.SetActive(false);
            if(GameManager.Instance.GetCardByID(GameManager.Instance.selectedCardUnlock.unlockCardID) != null)
            {
                Debug.Log("Udah pernah keunlock");
                return;
            }

            Debug.Log("benar");
            var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.cardListHolder.transform);
            generatedCard.transform.GetComponent<Card>().cardDetail = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedCardUnlock.unlockCardID);
            generatedCard.transform.GetComponent<Image>().sprite = generatedCard.GetComponent<Card>().cardDetail.cardSprite;


            //Misal terunlock, maka kartu akan hilang
            silangButton.SetActive(false);
            Destroy(GameManager.Instance.GetCardByID(GameManager.Instance.selectedCardUnlock.cardID));
            GameManager.Instance.player.GetDiscard(1);
            GameManager.Instance.selectedCardUnlock = null;
            GameManager.Instance.unlockCardImageSelected.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
        }
        else
        {
            warning.SetActive(false);
            PenaltyPanel.SetActive(true);
            Debug.Log("Salah");
            GameManager.Instance.player.getPenalty(180);
        }
    }

    public void SelectCardChoice()
    {
        GameManager.Instance.panelChoiceCard.SetActive(true);
    }

    public void OpenPanelUnlock()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }
}
