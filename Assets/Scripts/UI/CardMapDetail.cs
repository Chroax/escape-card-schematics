using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CardMapDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
{


    [SerializeField] private GameObject card;
    private Card cardScript;

    void Awake()
    {
        cardScript = card.GetComponent<Card>();
    }

    // Update is called once per frame
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale += new Vector3(0.2f, 0.2f, 0f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale -= new Vector3(0.2f, 0.2f, 0f);
    }
    public void OnPointerDown(PointerEventData eventData)
    {

        cardScript.panelCard.SetActive(true);
        cardScript.imageDetail.sprite = cardScript.cards.sprite;
    }
}
