using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombineCardPanel : MonoBehaviour
{
    public GameObject warning;
    public GameObject PenaltyPanel;
    public GameObject silangButton1;
    public GameObject silangButton2;
    public GameObject notification;

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

    public void RemoveCard1FromHolder()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        silangButton1.SetActive(false);
        GameManager.Instance.selectedCombineCard1 = null;
        GameManager.Instance.combineCardImageSelectedRed.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
    }

    public void RemoveCard2FromHolder()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        silangButton2.SetActive(false);
        GameManager.Instance.selectedCombineCard2 = null;
        GameManager.Instance.combineCardImageSelectedBlue.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
    }
    

    public void CombineCardSubmit()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
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

            Debug.Log("tercombine");
            selectedCardDetails = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedCombineCard1.cardID);
            combinedCardProducedDetails = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedCombineCard1.combineCardsProducesID[0]);
            GameManager.Instance.combineCardProducedImage.GetComponent<Image>().sprite = combinedCardProducedDetails.cardSprite;

            //Misal terunlock, maka kartu akan hilang
            silangButton1.SetActive(false);
            silangButton2.SetActive(false);

            foreach (string id in combinedCardProducedDetails.destroyedCardID)
            {
                Player.instance.saveData.ownedCardId.Remove(id);
                Player.instance.ownedCardId.Remove(id);
                Destroy(GameManager.Instance.GetCardByID(id));
                GameManager.Instance.listCardHolder.GetComponent<ListCard>().DeleteCardFromList(id);
                Player.instance.currentDiscard++;
                Player.instance.saveData.currentDiscard++;
                Player.instance.discUI.SetDiscard(Player.instance.currentDiscard);
                Player.instance.saveData.score += 5;
                Player.instance.score += 5;
            }

            GameManager.Instance.selectedCombineCard1 = null;
            GameManager.Instance.selectedCombineCard2 = null;
            GameManager.Instance.combineCardImageSelectedRed.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
            GameManager.Instance.combineCardImageSelectedBlue.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
            notification.SetActive(true);
            notification.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = selectedCardDetails.combineCardsProducesID.Count.ToString();

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
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        GameManager.Instance.listCardHolder.SetActive(true);
        GameManager.Instance.choiceCombineCard1 = true;
        GameManager.Instance.choiceCombineCard2 = false;
    }

    public void SelectCardChoice2()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        GameManager.Instance.listCardHolder.SetActive(true);
        GameManager.Instance.choiceCombineCard2 = true;
        GameManager.Instance.choiceCombineCard1 = false;
    }

    public void CombinedCardCollect()
    {
        if (!cardCollected)
        {
            GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
            notification.SetActive(false);
            foreach (string id in selectedCardDetails.combineCardsProducesID)
            {
                var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.deckCardHolder.transform);
                generatedCard.transform.GetComponent<Card>().cardDetail = GameManager.Instance.GetCardDetailByID(id);
                if (generatedCard.transform.GetComponent<Card>().cardDetail.cardType == CardType.map)
                {
                    cardPanel.ChangePanel(generatedCard.GetComponent<Card>().cardDetail.mapIndex);
                    Destroy(generatedCard);
                }
                else
                {
                    GameManager.Instance.listCardHolder.GetComponent<ListCard>().AddCardToList(id);
                    Player.instance.saveData.ownedCardId.Add(id);
                    Player.instance.ownedCardId.Add(generatedCard.transform.GetComponent<Card>().cardDetail.cardID);
                }
            }
            cardCollected = true;
            GameManager.Instance.combineCardProducedImage.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
        }
    }

    public void SelectCardChoice()
    {
        GameManager.Instance.listCardHolder.SetActive(true);
    }

    public void OpenPanelCombine()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }

    public void OnDisable()
    {
        GameManager.Instance.warningCombine.SetActive(false);
        if(GameManager.Instance.selectedCombineCard2 != null)
            this.RemoveCard2FromHolder();
        if (GameManager.Instance.selectedCombineCard1 != null)
            this.RemoveCard1FromHolder();
    }
    private void Update()
    {
        if (GameManager.Instance.selectedCombineCard1 != null)
        {
            silangButton1.SetActive(true);
        }

        if (GameManager.Instance.selectedCombineCard2 != null)
        {
            silangButton2.SetActive(true);
        }
    }
}
