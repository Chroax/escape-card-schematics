using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextEvent : MonoBehaviour
{
    private TMP_Text text;
    [SerializeField] private List<string> dialoges;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void ChangeTextEvent(int index)
    {
        if(dialoges[index] != null)
            text.text = dialoges[index];
    }
}
