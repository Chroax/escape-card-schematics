using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionMaker : MonoBehaviour
{
    public GameObject PotionOneIcon;
    public GameObject PotionTwoIcon;
    public GameObject PotionThreeIcon;
    public GameObject PotionFourIcon;
    public GameObject PotionFiveIcon;
    public GameObject infoPanel;

    public void OnAndOffPotionOneButton()
    {
        //GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!PotionOneIcon.activeInHierarchy)
            PotionOneIcon.SetActive(true);
        else
            PotionOneIcon.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void OnAndOffPotionTwoButton()
    {
        //GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!PotionTwoIcon.activeInHierarchy)
            PotionTwoIcon.SetActive(true);
        else
            PotionTwoIcon.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void OnAndOffPotionThreeButton()
    {
       // GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!PotionThreeIcon.activeInHierarchy)
            PotionThreeIcon.SetActive(true);
        else
            PotionThreeIcon.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void OnAndOffPotionFourButton()
    {
        //GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!PotionFourIcon.activeInHierarchy)
            PotionFourIcon.SetActive(true);
        else
            PotionFourIcon.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void OnAndOffPotionFiveButton()
    {
        //GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!PotionFiveIcon.activeInHierarchy)
            PotionFiveIcon.SetActive(true);
        else
            PotionFiveIcon.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void Submit()
    {
        PotionOneIcon.SetActive(false);
        PotionTwoIcon.SetActive(false);
        PotionThreeIcon.SetActive(false);
        PotionFourIcon.SetActive(false);
        PotionFiveIcon.SetActive(false);
        infoPanel.SetActive(true);
    }

    public void ResetButton()
    {
        PotionOneIcon.SetActive(false);
        PotionTwoIcon.SetActive(false);
        PotionThreeIcon.SetActive(false);
        PotionFourIcon.SetActive(false);
        PotionFiveIcon.SetActive(false);
        infoPanel.SetActive(true);
    }
}
