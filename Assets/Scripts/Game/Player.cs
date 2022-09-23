using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] private CoinSystem coinUI;
    [SerializeField] private TimeSystem timeUI;
    [SerializeField] public DiscardSystem discUI;
    [SerializeField] private TeamUI teamUI;
    [SerializeField] private ListCard listCard;

    public List<string> ownedCardId;

    private string teamName;
    public int mapIndex;
    public int currentCoin { get; set; }
    private float currentTime;
    public int currentDiscard;

    public GameObject timeOut;
    public int score { get; set; }
    public GameObject map;
    MapCardPanel mapCardPanel;
    public void Awake(){
        instance = this;
    }
    public void Init()
    {
        if(DBManager.isWin && !GameManager.Instance.isPenjelasan)
        {
            GameManager.Instance.winPanel.SetActive(true);
        }
        else
        {
            teamName = DBManager.team_name;
            mapIndex = DBManager.mapID;
            score = DBManager.scores;
            
            currentCoin = DBManager.remaining_coins;
            currentDiscard = DBManager.discardCardsCount;
            currentTime = DBManager.remaining_hours;

            mapCardPanel = map.GetComponent<MapCardPanel>();
            mapCardPanel.ChangePanel(mapIndex);
            
            teamUI.SetName(teamName);
            timeUI.SetTime(currentTime);
            coinUI.SetCoin(currentCoin);
            discUI.SetDiscard(currentDiscard);
            if (currentTime > 0)
            {
                foreach (string id in DBManager.ownedCards)
                    ownedCardId.Add(id);
                foreach (string id in ownedCardId)
                {
                    var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.deckCardHolder.transform);
                    generatedCard.transform.GetComponent<Card>().cardDetail = GameManager.Instance.GetCardDetailByID(id);
                    GameManager.Instance.listCardHolder.GetComponent<ListCard>().AddCardToList(id);
                }
            }
            else if(!GameManager.Instance.isPenjelasan)
            {
                if (GameManager.Instance.penaltyPanel.activeInHierarchy)
                    GameManager.Instance.penaltyPanel.SetActive(false);
                timeOut.SetActive(true);
                currentTime = 0;
                DBManager.remaining_hours = currentTime;
            }
            else
            {
                ownedCardId.Add("16");
                ownedCardId.Add("32");

                foreach (string id in ownedCardId)
                {
                    var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.deckCardHolder.transform);
                    generatedCard.transform.GetComponent<Card>().cardDetail = GameManager.Instance.GetCardDetailByID(id);
                    GameManager.Instance.listCardHolder.GetComponent<ListCard>().AddCardToList(id);
                }
            }
        }
    }
    void Update()
    {

        if (!DBManager.isWin && currentTime > 0 && !GameManager.Instance.isPenjelasan)
        {
            if(currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                DBManager.remaining_hours = currentTime;
                timeUI.SetTime(currentTime);
            }

            //penalty
            if (currentTime <= 0)
            {
                if (GameManager.Instance.penaltyPanel.activeInHierarchy)
                    GameManager.Instance.penaltyPanel.SetActive(false);
                timeOut.SetActive(true);
                currentTime = 0;
                DBManager.remaining_hours = currentTime;
            }
        }
    }

    public bool getPenalty(int time)
    {
        currentTime -= time;
        if(currentTime <= 0)
        {
            timeOut.SetActive(true);
            currentTime = 0;
            DBManager.remaining_hours = currentTime;
            return true;
        }
        timeUI.SetTime(currentTime);
        return false;
    }

    public bool UseCoin(int coin)
    {
        if(coin <= currentCoin)
        {
            currentCoin -= coin;
            DBManager.remaining_coins = currentCoin;
            coinUI.SetCoin(currentCoin);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetCoin(int coin)
    {
        currentCoin += coin;
        coinUI.SetCoin(currentCoin);
    }
}
