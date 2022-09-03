using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] public int defaultCoin;
    [SerializeField] public CoinSystem coinUI;
    [SerializeField] public float defaultTimer;
    [SerializeField] public TimeSystem timeUI;
    [SerializeField] public int defaultDiscard;
    [SerializeField] public DiscardSystem discUI;
    [SerializeField] public TeamUI teamUI;

    private string teamName;
    private GameObject[] discardCards;
    private GameObject[] ownedCards;

    private int currentCoin;
    private float currentTime;
    private int currentDiscard;

    public GameObject timeOut;

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
        currentTime -= Time.deltaTime;
        timeUI.SetTime(currentTime);
        if(currentTime <= 0)
        {
            timeOut.SetActive(true);
            currentTime = 0;
        }
        
        //debug
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            getPenalty(10);
            UseCoin(5);
            GetDiscard(1);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GetCoin(5);
        }
        */
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

    public void GetDiscard(int card)
    {
        currentDiscard += card;
        discUI.SetDiscard(currentDiscard);
    }
}
