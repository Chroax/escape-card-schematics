using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public CardDetailSO cardDetail;
    private RectTransform rectTransform;
    private bool isDraged = false;
    private Vector3 originPosition;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        transform.GetComponent<Image>().sprite = cardDetail.cardSprite;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse enter");
        transform.localScale += new Vector3(0.1f, 0.1f, 0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
        transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
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
            var cardPanel = Instantiate(GameResource.Instance.detailPanel, GameManager.Instance.panelTransform);
            cardPanel.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = cardDetail.cardSprite;
            cardPanel.transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = cardDetail.cardDescription;
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
