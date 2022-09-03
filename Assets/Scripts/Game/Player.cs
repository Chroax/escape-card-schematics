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

    private string teamName;
    public List<GameObject> discardCards;
    public List<GameObject> ownedCards;

    private int currentCoin;
    private float currentTime;
    private int currentDiscard;

    public GameObject timeOut;

    public void Awake(){instance = this;}
    public void Init()
    {
        discardCards = new List<GameObject>();
        ownedCards = new List<GameObject>();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            getPenalty(10);
            UseCoin(5);

            //test
            DiscardCards("36");
        }
        if (Input.GetKeyDown(KeyCode.Tab))
            GetCoin(5);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //test
            AddCards(tesObj1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //test
            AddCards(tesObj2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //test
            AddCards(tesObj3);
        }
    }

    private void AddCards(params GameObject[] cards)
    {
        foreach(GameObject card in cards)
            CardSpawner.instance.SetSpawn(card);
    }

    public void DiscardCards(params string[] ids)
    {
        foreach (string id in ids)
        {
            bool find = false;
            foreach(GameObject card in ownedCards)
            {
                Card details = card.GetComponent<Card>();
                if(details.cardDetail.cardID == id)
                {
                    CardSpawner.instance.DestroyCard(card);
                    currentDiscard++;
                    discUI.SetDiscard(currentDiscard);
                    find = true;
                    break;
                }
            }

            if (!find)
                Debug.Log("You dont have that card");
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
