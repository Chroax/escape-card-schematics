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
    [SerializeField] private DiscardSystem discUI;
    [SerializeField] private TeamUI teamUI;
    [SerializeField] private GameObject tesObj1;
    [SerializeField] private GameObject tesObj2;
    [SerializeField] private GameObject tesObj3;
    [SerializeField] private GameObject tesObj4;
    [SerializeField] private GameObject tesObj5;
    [SerializeField] private ListCard listCard;
    public GameObject BackyardMap;
    public GameObject GardenShedMap;
    public GameObject LivingRoomMap;
    public GameObject BasementMap;
    public GameObject IsolationRoomMap;
    public GameObject HallwayMap;

    private string teamName;

    private int currentCoin;
    private float currentTime;
    private int currentDiscard;

    public GameObject timeOut;

    private MapCard BackyardCards;
    private MapCard GardenShedCards;
    private MapCard LivingRoomCards;
    private MapCard BasementCards;
    private MapCard IsolationRoomCards;
    private MapCard HallwayCards;

    private bool isBackyardCard = true;
    private bool isGardenShedCard = true;
    private bool isLivingRoomCard = true;
    private bool isBasementCard = true;
    private bool isIsolationRoomCard = true;
    private bool isHallwayCard = true;
    public void Start()
    {
        foreach (GameObject card in BackyardCards.cardList) AddCards(card);
    }
    public void Awake(){
        instance = this;
        BackyardCards = BackyardMap.GetComponent<MapCard>();
        GardenShedCards = GardenShedMap.GetComponent<MapCard>();
        LivingRoomCards = LivingRoomMap.GetComponent<MapCard>();
        BasementCards = BasementMap.GetComponent<MapCard>();
        IsolationRoomCards = IsolationRoomMap.GetComponent<MapCard>();
        HallwayCards = HallwayMap.GetComponent<MapCard>();
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

            //test
            //DiscardCards("25");
        }
        if (Input.GetKeyDown(KeyCode.Tab))
            GetCoin(5);
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CardSpawner.instance.SetSpawn(tesObj1);
            listCard.AddCardToList(tesObj1);
            Debug.Log("berhasil1");
            //test
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CardSpawner.instance.SetSpawn(tesObj2);
            listCard.AddCardToList(tesObj2);
            Debug.Log("berhasil2");
            //test
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CardSpawner.instance.SetSpawn(tesObj3);
            listCard.AddCardToList(tesObj3);
            Debug.Log("berhasil3");
            //test
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CardSpawner.instance.SetSpawn(tesObj4);
            listCard.AddCardToList(tesObj4);
            Debug.Log("berhasil4");
            //test
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CardSpawner.instance.SetSpawn(tesObj5);
            listCard.AddCardToList(tesObj5);
            Debug.Log("berhasil5");
            //test
        }
        */
        if (isGardenShedCard && GameManager.Instance.activeMap == ActiveMap.BackyardandGardenShed)
        {
            foreach (GameObject card in GardenShedCards.cardList) AddCards(card);
            isGardenShedCard = false;
        }

        else if (isLivingRoomCard && GameManager.Instance.activeMap == ActiveMap.LivingRoomandKitchen)
        {
            foreach (GameObject card in LivingRoomCards.cardList) AddCards(card);
            isLivingRoomCard = false;
        }
        else if (isBasementCard && GameManager.Instance.activeMap == ActiveMap.Basement)
        {
            foreach (GameObject card in BasementCards.cardList) AddCards(card);
            isBasementCard = false;
        }
        else if (isIsolationRoomCard && GameManager.Instance.activeMap == ActiveMap.IsolationRoom)
        {
            foreach (GameObject card in IsolationRoomCards.cardList) AddCards(card);
            isIsolationRoomCard = false;
        }
        else if (isHallwayCard && GameManager.Instance.activeMap == ActiveMap.Hallway)
        {
            foreach (GameObject card in HallwayCards.cardList) AddCards(card);
            isHallwayCard = false;
        }
    }

    public void DiscardCards(params string[] ids)
    {
        foreach (string id in ids)
        {
            CardSpawner.instance.DestroyCard(id);
            listCard.DeleteCardFromList(id);
            currentDiscard++;
            discUI.SetDiscard(currentDiscard);
        }
    }
    public void AddCards(params GameObject[] cards)
    {
        foreach (GameObject card in cards)
        {
            CardSpawner.instance.SetSpawn(card);
            listCard.AddCardToList(card);
        }
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
