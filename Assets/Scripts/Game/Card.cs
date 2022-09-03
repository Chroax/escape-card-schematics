using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image cards;
    public CardDetailSO cardDetail;
    public Image imageDetail;
    private RectTransform rectTransform;
    private bool isDraged = false;
    private Vector3 originPosition;
    private CanvasGroup canvasGroup;

    public GameObject panelCard;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        cards.sprite = cardDetail.cardSprite;
    }

    Vector3 temp = new Vector3(30f, 0, 0);

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse enter");
        transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        this.transform.position -= temp;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
        transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
        this.transform.position += temp;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        isDraged = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GameManager.Instance.canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDraged = false;
        originPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDraged)
        {
            panelCard.SetActive(true);
            imageDetail.sprite = cards.sprite;
        }
        else
        {
            if (true)
            {
                rectTransform.anchoredPosition = originPosition;
            }
        }
    }
}
