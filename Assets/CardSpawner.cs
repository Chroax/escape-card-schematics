using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSpawner : MonoBehaviour
{
    public GameObject objToSpawn;

    public void SetSpawn()
    {
        Instantiate(objToSpawn, transform);
    }
}
