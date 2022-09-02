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

    private int currentCoin;
    private float currentTime;

    public GameObject timeOut;

    public void Init()
    {
        currentCoin = defaultCoin;
        currentTime = defaultTimer;
        timeUI.SetTime(currentTime);
        coinUI.SetCoin(currentCoin);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            getPenalty(10);
            UseCoin(5);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GetCoin(5);
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
