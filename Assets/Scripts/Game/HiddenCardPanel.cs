using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HiddenCardPanel : MonoBehaviour
{
    public TMP_InputField inputText;
    public CardDetailSO cardDetail;

    private void OnEnable()
    {
        GameManager.Instance.activePanel = ActivePanel.hidden;
    }

    public void HiddenCardSubmit()
    {
        if(GameManager.Instance.selectedCardHidden.hiddenCardID == inputText.text && inputText.text != "0")
        {
            if(GameManager.Instance.GetCardByID(inputText.text) != null)
            {
                Debug.Log("Udah pernah kebuka");
                return;
            }

            Debug.Log("Ketemu hiddennya");
            var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.cardListHolder.transform);
            generatedCard.transform.GetComponent<Card>().cardDetail = GameManager.Instance.GetCardDetailByID(inputText.text);

            // Misal kartu terhidden, maka kartu akan hilang
            //Destroy(GameManager.Instance.GetCardByID(GameManager.Instance.selectedCard.cardID));

        }
        else
        {
            Debug.Log("ga nemu");
            var cardPanel = Instantiate(GameResource.Instance.detailPanel, GameManager.Instance.panelTransform);
            cardPanel.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = cardDetail.cardSprite;
            cardPanel.transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = cardDetail.cardDescription;
        }
    }

    public void SelectCardChoice()
    {
        GameManager.Instance.panelChoiceCard.SetActive(true);
    }

    public void OpenPanelHidden()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }
}
