using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockCardPanel : MonoBehaviour
{
    public TMP_InputField inputText;
    public GameObject warning;
    public GameObject PenaltyPanel;
    public GameObject silangButton;
    public GameObject map;
    MapCardPanel cardPanel;
    private CardDetailSO produceCardDetail;

    private void Awake()
    {
        cardPanel = map.GetComponent<MapCardPanel>();
    }

    private void OnEnable()
    {
        GameManager.Instance.activePanel = ActivePanel.unlock;
    }

    public void RemoveCardFromHolder()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        silangButton.SetActive(false);
        GameManager.Instance.selectedCardUnlock = null;
        GameManager.Instance.unlockCardImageSelected.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
        inputText.text=  "";
    }

    public void UnlockCardSubmit()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        if (GameManager.Instance.selectedCardUnlock == null)
        {
            warning.SetActive(true);
            return;
        }
        else if (GameManager.Instance.selectedCardUnlock.unlockCardAnswer == inputText.text && inputText.text != "0" && inputText.text != "")
        {
            warning.SetActive(false);
            if(CardSpawner.instance.GetCardByID(GameManager.Instance.selectedCardUnlock.unlockCardProducesID[0], CardSpawner.instance.spawnRoots) != null)
            {
                return;
            }

            //Debug.Log(GameManager.Instance.selectedCardUnlock.unlockCardProducesID[0]);
            if (GameManager.Instance.selectedCardUnlock.unlockCardProducesID[0] == "WIN")
            {
                DBManager.discardCardsCount += Player.instance.ownedCardId.Count;
                DBManager.scores += Player.instance.ownedCardId.Count * 5 + Player.instance.currentCoin;
                if (DBManager.ownedCards.Contains("31"))
                    DBManager.scores += 50;
                DBManager.ownedCards.Clear();
                DBManager.remaining_hours = 0;
                Player.instance.currentTime = 0;
                Player.instance.isWin = true;
            }
            else
            {
                produceCardDetail = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedCardUnlock.unlockCardProducesID[0]);
                
                foreach (string id in produceCardDetail.destroyedCardID)
                {
                    produceCardDetail = GameManager.Instance.GetCardDetailByID(id);
                    if (GameManager.Instance.GetCardByID(id) == null)
                    {
                        GameManager.Instance.player.getPenalty(180);
                        if(DBManager.remaining_hours>0)
                            PenaltyPanel.SetActive(true);
                        return;
                    }
                }
                foreach (string id in GameManager.Instance.selectedCardUnlock.unlockCardProducesID)
                {
                    produceCardDetail = GameManager.Instance.GetCardDetailByID(id);
                    var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.deckCardHolder.transform);
                    generatedCard.transform.GetComponent<Card>().cardDetail = produceCardDetail;
                    generatedCard.transform.GetComponent<Image>().sprite = produceCardDetail.cardSprite;

                    inputText.text = "";

                    if (produceCardDetail.cardType == CardType.map)
                    {
                        cardPanel.ChangePanel(produceCardDetail.mapIndex);
                        Destroy(generatedCard);
                    }
                    else
                    {
                        Player.instance.ownedCardId.Add(generatedCard.transform.GetComponent<Card>().cardDetail.cardID);
                        DBManager.ownedCards.Add(id);
                        GameManager.Instance.listCardHolder.GetComponent<ListCard>().AddCardToList(produceCardDetail.cardID);
                    }
                }

                //Misal terunlock, maka kartu akan hilang
                silangButton.SetActive(false);
                foreach (string id in produceCardDetail.destroyedCardID)
                {
                    DBManager.ownedCards.Remove(id);
                    Player.instance.ownedCardId.Remove(id);
                    Destroy(GameManager.Instance.GetCardByID(id));
                    GameManager.Instance.listCardHolder.GetComponent<ListCard>().DeleteCardFromList(id);
                    Player.instance.currentDiscard++;
                    DBManager.discardCardsCount++;
                    Player.instance.discUI.SetDiscard(Player.instance.currentDiscard);
                    DBManager.scores += 5;
                    Player.instance.score += 5;
                }

                GameManager.Instance.selectedCardUnlock = null;
                GameManager.Instance.unlockCardImageSelected.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;

            }
        }
        else
        {
            warning.SetActive(false);
            GameManager.Instance.player.getPenalty(180);
            if(DBManager.remaining_hours>0) PenaltyPanel.SetActive(true);
        }
    }
    public void SelectCardChoice()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        GameManager.Instance.listCardHolder.SetActive(true);
    }
    public void OpenPanelUnlock()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }
    private void OnDisable(){
        if(!DBManager.isWin)
        {
            if (GameManager.Instance.selectedCardUnlock != null)
                this.RemoveCardFromHolder();
            GameManager.Instance.warningUnlock.SetActive(false);
        }
    }
    private void Update(){
        if(GameManager.Instance.selectedCardUnlock != null){
            silangButton.SetActive(true);
        }
    }
}
