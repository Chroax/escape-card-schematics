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
    private int currentCoin;
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
        teamName = "OK";
        mapIndex = 0;
        score = 0;
        ownedCardId.Add("25");
        ownedCardId.Add("21");
        ownedCardId.Add("15");
        ownedCardId.Add("M");
        ownedCardId.Add("5");
        currentCoin = 0;
        currentDiscard = 0;
        currentTime = 3600;

        mapCardPanel = map.GetComponent<MapCardPanel>();
        mapCardPanel.ChangePanel(mapIndex);
        foreach (string id in ownedCardId)
        {
            var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.deckCardHolder.transform);
            generatedCard.transform.GetComponent<Card>().cardDetail = GameManager.Instance.GetCardDetailByID(id);
            GameManager.Instance.listCardHolder.GetComponent<ListCard>().AddCardToList(id);
        }
        teamUI.SetName(teamName);
        timeUI.SetTime(currentTime);
        coinUI.SetCoin(currentCoin);
        discUI.SetDiscard(currentDiscard);
    }
    /*
    public void Init()
    {
        teamName = DBManager.team_name;
        mapIndex = DBManager.mapID;
        score = DBManager.scores;
        foreach (string id in DBManager.ownedCards)
            ownedCardId.Add(id);
        currentCoin = DBManager.remaining_coins;
        currentDiscard = DBManager.discardCardsCount;
        currentTime = DBManager.remaining_hours;

        mapCardPanel = map.GetComponent<MapCardPanel>();
        mapCardPanel.ChangePanel(mapIndex);
        foreach (string id in ownedCardId)
        {
            var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.deckCardHolder.transform);
            generatedCard.transform.GetComponent<Card>().cardDetail = GameManager.Instance.GetCardDetailByID(id);
            GameManager.Instance.listCardHolder.GetComponent<ListCard>().AddCardToList(id);
        }
        teamUI.SetName(teamName);
        timeUI.SetTime(currentTime);
        coinUI.SetCoin(currentCoin);
        discUI.SetDiscard(currentDiscard);
    }
    */
    void Update()
    {   
        currentTime -= Time.deltaTime;
        DBManager.remaining_hours = currentTime;
        timeUI.SetTime(currentTime);
        
        //penalty
        if(currentTime <= 0)
        {
            if (GameManager.Instance.penaltyPanel.activeInHierarchy)
                GameManager.Instance.penaltyPanel.SetActive(false);
            timeOut.SetActive(true);
            currentTime = 0;
        }
        
        //debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            getPenalty(10);
            UseCoin(5);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
            GetCoin(5);
    }

    public bool getPenalty(int time)
    {
        currentTime -= time;
        timeUI.SetTime(currentTime);
        if(currentTime <= 0)
        {
            timeOut.SetActive(true);
            currentTime = 0;
            return true;
        }
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
