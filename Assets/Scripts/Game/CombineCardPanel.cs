using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineCardPanel : MonoBehaviour
{
    public GameObject warning;
    public GameObject PenaltyPanel;
    public GameObject silangButton1;
    public GameObject silangButton2;

    private CardDetailSO combinedCardProducedDetails;
    private CardDetailSO selectedCardDetails;
    private bool cardCollected = true;
    public GameObject map;
    MapCardPanel cardPanel;
    private void Awake()
    {
        cardPanel = map.GetComponent<MapCardPanel>();
    }

    private void OnEnable()
    {
        GameManager.Instance.activePanel = ActivePanel.combine;
    }

    public void removeCard1FromHolder()
    {
        silangButton1.SetActive(false);
        GameManager.Instance.selectedCombineCard1 = null;
        GameManager.Instance.combineCardImageSelected1.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
    }

    public void removeCard2FromHolder()
    {
        silangButton2.SetActive(false);
        GameManager.Instance.selectedCombineCard2 = null;
        GameManager.Instance.combineCardImageSelected2.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
    }
    

    public void CombineCardSubmit()
    {
        if (GameManager.Instance.selectedCombineCard1 == null || GameManager.Instance.selectedCombineCard2 == null)
        {
            warning.SetActive(true);
            return;
        }

        bool sameProduce = true;
        foreach(string id in GameManager.Instance.selectedCombineCard1.combineCardsProducesID)
        {
            if (!GameManager.Instance.selectedCombineCard2.combineCardsProducesID.Contains(id)){
                sameProduce = false;
                break;
            }
        }

        if (sameProduce && GameManager.Instance.selectedCombineCard1.combineCardsProducesID[0] != "0" 
                && GameManager.Instance.selectedCombineCard1.cardID != GameManager.Instance.selectedCombineCard2.cardID)
        {
            Debug.Log("tercombine");
            if (!cardCollected)
            {
                Debug.Log("Ambil dulu kartu hasil combine");
                warning.SetActive(true);
            }

            foreach (string id in GameManager.Instance.selectedCombineCard1.destroyedCardID)
            {
                if (GameManager.Instance.GetCardByID(id) == null)
                {
                    Debug.Log("Clue yang dikumpulkan belum cukup");
                    return;
                }
            }

            Debug.Log("tercombine");

            selectedCardDetails = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedCombineCard1.cardID);
            combinedCardProducedDetails = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedCombineCard1.combineCardsProducesID[0]);
            GameManager.Instance.combineCardProducedImage.GetComponent<Image>().sprite = combinedCardProducedDetails.cardSprite;

            if (combinedCardProducedDetails.cardType == CardType.map)
            {
                cardPanel.changepanel();
            }

            //Misal terunlock, maka kartu akan hilang
            silangButton1.SetActive(false);
            silangButton2.SetActive(false);

            foreach (string id in combinedCardProducedDetails.destroyedCardID)
            {
                Destroy(GameManager.Instance.GetCardByID(id));
                GameManager.Instance.panelChoiceCard.GetComponent<ListCard>().DeleteCardFromList(id);
                Player.instance.currentDiscard++;
                Player.instance.discUI.SetDiscard(Player.instance.currentDiscard);
            }

            GameManager.Instance.selectedCombineCard1 = null;
            GameManager.Instance.selectedCombineCard2 = null;
            GameManager.Instance.combineCardImageSelected1.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
            GameManager.Instance.combineCardImageSelected2.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;

            cardCollected = false;
        }
        else
        {
            Debug.Log("PIPIP KENAK PENALTY!!!!");
            warning.SetActive(false);
            PenaltyPanel.SetActive(true);
            Debug.Log("Salah");
            GameManager.Instance.player.getPenalty(180);
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
            foreach(string id in selectedCardDetails.combineCardsProducesID)
            {

                var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.cardListHolder.transform);
                generatedCard.transform.GetComponent<Card>().cardDetail = GameManager.Instance.GetCardDetailByID(id);
                if (generatedCard.transform.GetComponent<Card>().cardDetail.cardType == CardType.map)
                    Destroy(generatedCard);
                else
                    GameManager.Instance.panelChoiceCard.GetComponent<ListCard>().AddCardToList(id);
            }
            cardCollected = true;
            GameManager.Instance.combineCardProducedImage.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
        }
    }

    public void SelectCardChoice()
    {
        GameManager.Instance.panelChoiceCard.SetActive(true);
    }

    public void OpenPanelCombine()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }

    public void OnDisable()
    {
        GameManager.Instance.warningCombine.SetActive(false);
        this.removeCard1FromHolder();
        this.removeCard2FromHolder();
    }
}
