using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListCard : MonoBehaviour
{
    public static ListCard instance;
    public GameObject redList;
    public GameObject blueList;
    public GameObject yellowList;
    public GameObject greyList;
    public GameObject greenList;
    public Transform cardRedList;
    public Transform cardBlueList;
    public Transform cardYellowList;
    public Transform cardGreyList;
    public Transform cardGreenList;

    private void Awake(){ instance = this; }
    public void CloseAllListPanel()
    {
        redList.SetActive(false);
        blueList.SetActive(false);
        yellowList.SetActive(false);
        greyList.SetActive(false);
        greenList.SetActive(false);
    }

    public void OpenRedCard()
    {
        CloseAllListPanel();
        redList.SetActive(true);
    }

    public void OpenBlueCard()
    {
        CloseAllListPanel();
        blueList.SetActive(true);
    }

    public void OpenYellowCard()
    {
        CloseAllListPanel();
        yellowList.SetActive(true);
    }

    public void OpenGreyCard()
    {
        CloseAllListPanel();
        greyList.SetActive(true);
    }

    public void OpenGreenCard()
    {
        CloseAllListPanel();
        greenList.SetActive(true);
    }

    public void AddCardToList(GameObject card)
    {
        switch (card.GetComponent<Card>().cardDetail.cardType)
        {
            case CardType.red:
                Instantiate(card, cardRedList);
                break;
            case CardType.blue:
                Instantiate(card, cardBlueList);
                break;
            case CardType.yellow:
                Instantiate(card, cardYellowList);
                break;
            case CardType.grey:
                Instantiate(card, cardGreyList);
                break;
            case CardType.green:
                Instantiate(card, cardGreenList);
                break;
        }
    }

    public void DeleteCardFromList(string id)
    {
        GameObject card = CardSpawner.instance.GetCardByID(id, CardSpawner.instance.spawnRoots);
        switch (card.GetComponent<Card>().cardDetail.cardType)
        {
            case CardType.red:
                GameObject findRedCard = CardSpawner.instance.GetCardByID(id, cardRedList);
                Destroy(findRedCard);
                break;
            case CardType.blue:
                GameObject findBlueCard = CardSpawner.instance.GetCardByID(id, cardBlueList);
                Destroy(findBlueCard);
                break;
            case CardType.yellow:
                GameObject findYellowCard = CardSpawner.instance.GetCardByID(id, cardYellowList);
                Destroy(findYellowCard);
                break;
            case CardType.grey:
                GameObject findGreyCard = CardSpawner.instance.GetCardByID(id, cardGreyList);
                Destroy(findGreyCard);
                break;
            case CardType.green:
                GameObject findGreenCard = CardSpawner.instance.GetCardByID(id, cardGreenList);
                Destroy(findGreenCard);
                break;
        }
    }
}
