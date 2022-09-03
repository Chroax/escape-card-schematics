using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSpawner : MonoBehaviour
{
    public static CardSpawner instance;
    void Awake() { instance = this; }
    public Transform spawnRoots;
    public Transform discardRoots;
    public Transform redList;
    public Transform blueList;
    public Transform yellowList;
    public Transform greyList;
    public Transform greenList;
    public void SetSpawn(GameObject objToSpawn)
    {
        Instantiate(objToSpawn, spawnRoots);
    }

    public void DestroyCard(string id)
    {
        GameObject objToDestroy = GetCardByID(id, spawnRoots);
        if(objToDestroy != null)
        {
            Instantiate(objToDestroy, discardRoots);
            Destroy(objToDestroy);
        }
        else
            Debug.Log("Card Not Found");
    }

    public GameObject GetCardByID(string cardID, Transform transform)
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Card>().cardDetail.cardID == cardID)
            {
                return child.gameObject;
            }
        }
        return null;
    }
}
