using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class GameManager : SingletonMonobehaviour<GameManager>
{
    // Dependencies References

    // General reference
    #region Header General References
    [Space(10)]
    [Header("General References")]
    #endregion
    public GameObject cardListHolder;
    [HideInInspector] public CardDetailSO selectedCardHidden;
    [HideInInspector] public CardDetailSO selectedCardUnlock;
    public GameObject panelChoiceCard;

    [HideInInspector] public CardDetailSO selectedCombineCard1;
    [HideInInspector] public CardDetailSO selectedCombineCard2;
    [HideInInspector] public bool choiceCombineCard1 = false;
    [HideInInspector] public bool choiceCombineCard2 = false;

    public Canvas canvas;
    public Sprite cardHolder;
    public List<CardDetailSO> allCardDetailList;
    public Transform panelTransform;

    // Keeps track which panel open
    [HideInInspector] public ActivePanel activePanel = ActivePanel.main;

    // Each Panel reference
    #region Header Panel References
    [Space(10)]
    [Header("Panels References")]
    #endregion
    public GameObject hiddenCardImageSelected;
    public GameObject hiddenCardPanel;
    public GameObject unlockCardImageSelected;
    public GameObject unlockCardPanel;
    public GameObject combineCardImageSelected1;
    public GameObject combineCardImageSelected2;
    public GameObject combineCardProducedImage;
    public GameObject combineCardPanel;
    public GameObject BackyardMapPanel;
    public GameObject machineCardPanel;
    


    #region Header Card Type Panel Settings
    [Space(10)]
    [Header("Card Type Per Panel")]
    #endregion
    public CardType hiddenCardType;
    public CardType unlockCardType;
    public CardType combineCardType1;
    public CardType combineCardType2;

    public CardDetailSO GetCardDetailByID(string cardID)
    {
        foreach(CardDetailSO cardDetail in allCardDetailList)
        {
            if(cardDetail.cardID == cardID)
            {
                return cardDetail;
            }
        }

        return null;
    }

    // Existing Card
    public GameObject GetCardByID(string cardID)
    {
        foreach(Transform child in cardListHolder.transform)
        {
            if(child.GetComponent<Card>().cardDetail.cardID == cardID)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public void CloseAllPanel()
    {
        hiddenCardPanel.SetActive(false);
        unlockCardPanel.SetActive(false);
        combineCardPanel.SetActive(false);
        machineCardPanel.SetActive(false);
        BackyardMapPanel.SetActive(false);
    }
}
