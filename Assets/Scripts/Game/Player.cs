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
    [SerializeField] public SaveDataSO saveData;

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
        teamName = DBManager.username;
        mapIndex = saveData.mapIndex;
        score = saveData.score;
        foreach (string id in saveData.ownedCardId)
            ownedCardId.Add(id);
        currentCoin = saveData.currentCoin;
        currentDiscard = saveData.currentDiscard;
        currentTime = saveData.currentTime;

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

    void Update()
    {   
        //countdown
        currentTime -= Time.deltaTime;
        saveData.currentTime = currentTime;
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
            saveData.currentCoin = currentCoin;
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
