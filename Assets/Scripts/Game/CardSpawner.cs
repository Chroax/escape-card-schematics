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

    public void SetSpawn(GameObject objToSpawn)
    {
        Player.instance.ownedCards.Add(Instantiate(objToSpawn, spawnRoots));
    }

    public void DestroyCard(GameObject objToDestroy)
    {
        Player.instance.discardCards.Add(Instantiate(objToDestroy, discardRoots));
        Player.instance.ownedCards.Remove(objToDestroy);
        Destroy(objToDestroy);
    }
}
