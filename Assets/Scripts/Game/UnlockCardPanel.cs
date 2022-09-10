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

    public void removeCardFromHolder()
    {
        silangButton.SetActive(false);
        GameManager.Instance.selectedCardUnlock = null;
        GameManager.Instance.unlockCardImageSelected.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
        inputText.text=  "";
    }

    public void UnlockCardSubmit()
    {
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
                Debug.Log("Udah pernah keunlock");
                return;
            }
            foreach(string id in GameManager.Instance.selectedCardUnlock.destroyedCardID)
            {
                if(GameManager.Instance.GetCardByID(id) == null)
                {
                    Debug.Log("Clue yang dikumpulkan belum cukup");
                    return;
                }
            }

            Debug.Log("benar");
            foreach(string id in GameManager.Instance.selectedCardUnlock.unlockCardProducesID)
            {
                produceCardDetail = GameManager.Instance.GetCardDetailByID(id);
                var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.cardListHolder.transform);
                generatedCard.transform.GetComponent<Card>().cardDetail = produceCardDetail;
                generatedCard.transform.GetComponent<Image>().sprite = produceCardDetail.cardSprite;

                inputText.text = "";

                if (produceCardDetail.cardType == CardType.map)
                {
                    cardPanel.changepanel();
                    Destroy(generatedCard);
                }
                else
                    GameManager.Instance.panelChoiceCard.GetComponent<ListCard>().AddCardToList(produceCardDetail.cardID);
            }

            //Misal terunlock, maka kartu akan hilang
            silangButton.SetActive(false);
            //Destroy(GameManager.Instance.GetCardByID(GameManager.Instance.selectedCardUnlock.cardID));
            foreach (string id in produceCardDetail.destroyedCardID)
            {
                Destroy(GameManager.Instance.GetCardByID(id));
                GameManager.Instance.panelChoiceCard.GetComponent<ListCard>().DeleteCardFromList(id);
                Player.instance.currentDiscard++;
                Player.instance.discUI.SetDiscard(Player.instance.currentDiscard);
            }

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
    private void OnDisable(){
        this.removeCardFromHolder();
        GameManager.Instance.warningUnlock.SetActive(false);
    }
    private void Update(){
        if(GameManager.Instance.selectedCardUnlock != null){
            silangButton.SetActive(true);
        }
    }
}
