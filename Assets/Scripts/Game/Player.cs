using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] private int defaultCoin;
    [SerializeField] private CoinSystem coinUI;
    [SerializeField] private float defaultTimer;
    [SerializeField] private TimeSystem timeUI;
    [SerializeField] private int defaultDiscard;
    [SerializeField] public DiscardSystem discUI;
    [SerializeField] private TeamUI teamUI;
    [SerializeField] private ListCard listCard;
    [SerializeField] private CardDetailSO startMap;

    [HideInInspector] public List<string> ownedCardId;
    [HideInInspector] public List<string> discardCardId;

    private string teamName;
    public int mapIndex;
    private int currentCoin;
    private float currentTime;
    public int currentDiscard;

    public GameObject timeOut;
    public int score { get; set; }
    private MapCard BackyardCards;
    public GameObject map;
    MapCardPanel mapCardPanel;

    public void Start()
    {
        mapIndex = 0;
        score = 0;
        mapCardPanel = map.GetComponent<MapCardPanel>();
        mapCardPanel.ChangePanel(startMap.mapIndex);
        foreach (string id in startMap.unlockCardProducesID)
        {
            var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.deckCardHolder.transform);
            generatedCard.transform.GetComponent<Card>().cardDetail = GameManager.Instance.GetCardDetailByID(id);
            GameManager.Instance.listCardHolder.GetComponent<ListCard>().AddCardToList(id);
        }
    }
    public void Awake(){
        instance = this;
    }
    public void Init()
    {
        teamName = DBManager.username;
        currentCoin = defaultCoin;
        currentTime = defaultTimer;
        currentDiscard = defaultDiscard;

        teamUI.SetName(teamName);
        timeUI.SetTime(currentTime);
        coinUI.SetCoin(currentCoin);
        discUI.SetDiscard(currentDiscard);
    }

    void Update()
    {   
        //countdown
        currentTime -= Time.deltaTime;
        timeUI.SetTime(currentTime);
        
        //penalty
        if(currentTime <= 0)
        {
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
            coinUI.SetCoin(currentCoin);
            return true;
        }
        else
        {
            Debug.Log("Not Enough Coin");
            return false;
        }
    }

    public void GetCoin(int coin)
    {
        currentCoin += coin;
        coinUI.SetCoin(currentCoin);
    }
}
