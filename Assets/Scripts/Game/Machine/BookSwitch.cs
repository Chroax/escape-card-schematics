using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookSwitch : MonoBehaviour
{
    public GameObject on;
    public GameObject off;
    public TextMeshProUGUI sign;
    public GameObject penaltyPanel;
    private CardDetailSO produceCardDetail;
    MapCardPanel cardPanel;
    private void Awake()
    {
        cardPanel = GameManager.Instance.mapPanel.GetComponent<MapCardPanel>();
    }
    public void OnAndOff()
    {
        if(on.activeInHierarchy)
        {
            sign.text = "OFF";
            on.SetActive(false);
            off.SetActive(true);
        }
        else
        {
            sign.text = "ON";
            off.SetActive(false);
            on.SetActive(true);
        }
    }
    public void Submit()
    {
        if(on.activeInHierarchy && !off.activeInHierarchy)
        {
            GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
            produceCardDetail = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedMachineCard.unlockCardProducesID[0]);

            Debug.Log("benar");
            foreach (string id in GameManager.Instance.selectedMachineCard.unlockCardProducesID)
            {
                produceCardDetail = GameManager.Instance.GetCardDetailByID(id);
                var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.deckCardHolder.transform);
                generatedCard.transform.GetComponent<Card>().cardDetail = produceCardDetail;
                generatedCard.transform.GetComponent<Image>().sprite = produceCardDetail.cardSprite;

                if (produceCardDetail.cardType == CardType.map)
                {
                    cardPanel.ChangePanel(produceCardDetail.mapIndex);
                    Destroy(generatedCard);
                }
                else
                {
                    Player.instance.ownedCardId.Add(generatedCard.transform.GetComponent<Card>().cardDetail.cardID);
                    Player.instance.saveData.ownedCardId.Add(id);
                    GameManager.Instance.listCardHolder.GetComponent<ListCard>().AddCardToList(produceCardDetail.cardID);
                }
            }

            GameManager.Instance.machineCardPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
            foreach (string id in produceCardDetail.destroyedCardID)
            {
                Player.instance.saveData.ownedCardId.Remove(id);
                Player.instance.ownedCardId.Remove(id);
                Destroy(GameManager.Instance.GetCardByID(id));
                GameManager.Instance.listCardHolder.GetComponent<ListCard>().DeleteCardFromList(id);
                Player.instance.currentDiscard++;
                Player.instance.currentDiscard++;
                Player.instance.discUI.SetDiscard(Player.instance.currentDiscard);
                Player.instance.saveData.score += 5;
                Player.instance.score += 5;
            }
            GameManager.Instance.machineCardPanel.GetComponent<MachineCardPanel>().RemoveCardFromHolder();
            ResetButton();
        }
        else
        {
            penaltyPanel.SetActive(true);
            Debug.Log("Salah");
            GameManager.Instance.player.getPenalty(180);
            ResetButton();
        }
    }
    public void ResetButton()
    {
        sign.text = "OFF";
        off.SetActive(true);
        on.SetActive(false);
    }
}