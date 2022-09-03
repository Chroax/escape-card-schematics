using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineCardPanel : MonoBehaviour
{
    private CardDetailSO combinedCardProducedDetails;
    private bool cardCollected = true;

    private void OnEnable()
    {
        GameManager.Instance.activePanel = ActivePanel.combine;
    }

    public void CombineCardSubmit()
    {
        if (GameManager.Instance.selectedCombineCard1.combineCardsProducesID == GameManager.Instance.selectedCombineCard2.combineCardsProducesID && GameManager.Instance.selectedCombineCard1.combineCardsProducesID != "0" && 
            GameManager.Instance.selectedCombineCard1.cardID != GameManager.Instance.selectedCombineCard2.cardID)
        {
            if (!cardCollected)
            {
                Debug.Log("Ambil dulu kartu hasil combine");
            }

            if(GameManager.Instance.GetCardByID(GameManager.Instance.selectedCombineCard1.combineCardsProducesID) != null)
            {
                Debug.Log("Udah pernah kecombine");
                return;
            }

            Debug.Log("tercombine");
            combinedCardProducedDetails = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedCombineCard1.combineCardsProducesID);
            GameManager.Instance.combineCardProducedImage.GetComponent<Image>().sprite = combinedCardProducedDetails.cardSprite;
            cardCollected = false;

            // Misal dicombine, kartu yg tercombine akan hilang
            //Destroy(GameManager.Instance.GetCardByID(GameManager.Instance.selectedCombineCard1.cardID));
            //Destroy(GameManager.Instance.GetCardByID(GameManager.Instance.selectedCombineCard2.cardID));
        }
        else
        {
            Debug.Log("PIPIP KENAK PENALTY!!!!");
        }
    }

    public void SelectCardChoice1()
    {
        GameManager.Instance.panelChoiceCard.SetActive(true);
        GameManager.Instance.choiceCombineCard1 = true;
        GameManager.Instance.choiceCombineCard2 = false;
    }

    public void SelectCardChoice2()
    {
        GameManager.Instance.panelChoiceCard.SetActive(true);
        GameManager.Instance.choiceCombineCard2 = true;
        GameManager.Instance.choiceCombineCard1 = false;
    }

    public void CombinedCardCollect()
    {
        if (!cardCollected)
        {
            var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.cardListHolder.transform);
            generatedCard.transform.GetComponent<Card>().cardDetail = combinedCardProducedDetails;
            cardCollected = true;
            GameManager.Instance.combineCardProducedImage.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
        }
    }

    public void OpenPanelCombine()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }
}
