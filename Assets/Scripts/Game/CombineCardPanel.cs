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
    private bool cardCollected = true;
    private GameObject generatedCard;
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

        if (GameManager.Instance.selectedCombineCard1.combineCardsProducesID == GameManager.Instance.selectedCombineCard2.combineCardsProducesID 
                && GameManager.Instance.selectedCombineCard1.combineCardsProducesID != "0" 
                && GameManager.Instance.selectedCombineCard1.cardID != GameManager.Instance.selectedCombineCard2.cardID)
        {
            Debug.Log("tercombine");
            if (!cardCollected)
            {
                Debug.Log("Ambil dulu kartu hasil combine");
                warning.SetActive(true);
            }

            Debug.Log("tercombine");

            generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.cardListHolder.transform);
            generatedCard.transform.GetComponent<Card>().cardDetail = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedCombineCard1.combineCardsProducesID);
            generatedCard.transform.GetComponent<Image>().sprite = generatedCard.GetComponent<Card>().cardDetail.cardSprite;
            generatedCard.transform.GetComponent<Card>().panelCard = GameManager.Instance.cardDetailPanel;
            generatedCard.transform.GetComponent<Card>().imageDetail = GameManager.Instance.detailImageCard;
            generatedCard.transform.GetComponent<CardChoice>().cardDetail = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedCombineCard1.combineCardsProducesID);

            if (generatedCard.GetComponent<Card>().cardDetail.cardType == CardType.map)
            {
                cardPanel.changepanel();
            }

            GameManager.Instance.combineCardProducedImage.GetComponent<Image>().sprite = generatedCard.GetComponent<Card>().cardDetail.cardSprite;


             //Misal terunlock, maka kartu akan hilang
            silangButton1.SetActive(false);
            silangButton2.SetActive(false);
            Player.instance.DiscardCards(generatedCard.transform.GetComponent<CardChoice>().cardDetail.destroyedCardID);
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
            Player.instance.AddCards(generatedCard);
            Destroy(generatedCard);
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
