using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityPanel : MonoBehaviour
{
    public GameObject onRedButton;
    public GameObject onBrownButton;
    public GameObject onBlueButton;
    public GameObject onBlackButton;
    public GameObject onGreenButton;
    public GameObject onYellowButton;
    public void OnAndOffRedButton()
    {
        if (!onRedButton.activeInHierarchy)
            onRedButton.SetActive(true);
        else
            onRedButton.SetActive(false);
    }
    public void OnAndOffBrownButton()
    {
        if (!onBrownButton.activeInHierarchy)
            onBrownButton.SetActive(true);
        else
            onBrownButton.SetActive(false);
    }
    public void OnAndOffBlueButton()
    {
        if (!onBlueButton.activeInHierarchy)
            onBlueButton.SetActive(true);
        else
            onBlueButton.SetActive(false);
    }
    public void OnAndOffBlackButton()
    {
        if (!onBlackButton.activeInHierarchy)
            onBlackButton.SetActive(true);
        else
            onBlackButton.SetActive(false);
    }
    public void OnAndOffGreenButton()
    {
        if (!onGreenButton.activeInHierarchy)
            onGreenButton.SetActive(true);
        else
            onGreenButton.SetActive(false);
    }
    public void OnAndOffYellowButton()
    {
        if (!onYellowButton.activeInHierarchy)
            onYellowButton.SetActive(true);
        else
            onYellowButton.SetActive(false);
    }

    public void Submit()
    {
        if (onRedButton.activeInHierarchy && !onBrownButton.activeInHierarchy &&
            onBlueButton.activeInHierarchy && !onBlackButton.activeInHierarchy &&
            onGreenButton.activeInHierarchy && onYellowButton.activeInHierarchy)
        {
            Debug.Log("Jawaban anda benar");
            Debug.Log("Selamat");
        }
        else
        {
            Debug.Log("Jawaban anda salah");
            ResetButton();
        }
    }
    public void ResetButton()
    {
        onRedButton.SetActive(false);
        onBrownButton.SetActive(false);
        onBlueButton.SetActive(false);
        onBlackButton.SetActive(false);
        onGreenButton.SetActive(false);
        onYellowButton.SetActive(false);
    }
}
