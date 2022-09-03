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

    private string teamName;

    private int currentCoin;
    private float currentTime;
    private int currentDiscard;

    public GameObject timeOut;
    public void Awake(){instance = this;}
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
            DiscardCards("25");
        }
        if (Input.GetKeyDown(KeyCode.Tab))
            GetCoin(5);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CardSpawner.instance.SetSpawn(tesObj1);
            listCard.AddCardToList(tesObj1);
            //test
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CardSpawner.instance.SetSpawn(tesObj2);
            listCard.AddCardToList(tesObj2);
            //test
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CardSpawner.instance.SetSpawn(tesObj3);
            listCard.AddCardToList(tesObj3);
            //test
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CardSpawner.instance.SetSpawn(tesObj4);
            listCard.AddCardToList(tesObj4);
            //test
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CardSpawner.instance.SetSpawn(tesObj5);
            listCard.AddCardToList(tesObj5);
            //test
        }
    }

    public void DiscardCards(params string[] ids)
    {
        foreach (string id in ids)
        {
            CardSpawner.instance.DestroyCard(id);
            ListCard.instance.DeleteCardFromList(id);
            currentDiscard++;
            discUI.SetDiscard(currentDiscard);
        }
    }

    private bool getPenalty(int time)
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
