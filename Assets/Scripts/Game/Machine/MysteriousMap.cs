using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MysteriousMap : MonoBehaviour
{
    public GameObject arrowE;
    public GameObject arrowV;
    public GameObject arrowT;
    public GameObject arrowP;
    public GameObject arrowL;
    public GameObject arrowA;
    public GameObject arrowQ;
    public GameObject arrowH;
    public GameObject warningInfo;
    public TextMeshProUGUI textWarning;
    public void Start()
    {
        textWarning = warningInfo.GetComponent<TextMeshProUGUI>();
    }
    public void OnAndOffArrowE()
    {
        if (!arrowE.activeInHierarchy)
        {
            if (arrowL.activeInHierarchy || arrowT.activeInHierarchy)
            {
                if (arrowL.activeInHierarchy)
                    textWarning.text = "Anda sekarang berada di jalur L, anda harus kembali ke tempat semula";
                else if(arrowT.activeInHierarchy)
                    textWarning.text = "Anda sekarang berada di jalur T, anda harus kembali ke tempat semula";
                warningInfo.SetActive(true);
            }
            else
                arrowE.SetActive(true);
        }
        else
        {
            warningInfo.SetActive(false);
            arrowE.SetActive(false);
            arrowV.SetActive(false);
        }
    }
    public void OnAndOffArrowV()
    {
        if (!arrowV.activeInHierarchy && arrowE.activeInHierarchy)
        {
            if (!arrowE.activeInHierarchy)
            {
                textWarning.text = "Anda harus melewati jalur E terlebih dahulu";
                warningInfo.SetActive(true);
            }
            else
                arrowV.SetActive(true);
        }
        else
            arrowV.SetActive(false);
    }
    public void OnAndOffArrowT()
    {
        if (!arrowT.activeInHierarchy)
        {
            if (arrowL.activeInHierarchy || arrowE.activeInHierarchy)
            {
                if (arrowL.activeInHierarchy)
                    textWarning.text = "Anda sekarang berada di jalur L, anda harus kembali ke tempat semula";
                else if (arrowE.activeInHierarchy)
                    textWarning.text = "Anda sekarang berada di jalur E, anda harus kembali ke tempat semula";
                warningInfo.SetActive(true);
            }
            else
                arrowT.SetActive(true);
        }
        else
        {
            warningInfo.SetActive(false);
            arrowT.SetActive(false);
            arrowP.SetActive(false);
        }
    }
    public void OnAndOffArrowP()
    {
        if (!arrowP.activeInHierarchy)
        {
            if (!arrowT.activeInHierarchy)
            {
                textWarning.text = "Anda harus melewati jalur T terlebih dahulu";
                warningInfo.SetActive(true);
            }
            else
                arrowP.SetActive(true);
        }
        else
            arrowP.SetActive(false);
    }
    public void OnAndOffArrowL()
    {
        if (!arrowL.activeInHierarchy)
        {
            if (arrowT.activeInHierarchy || arrowE.activeInHierarchy)
            {
                if (arrowT.activeInHierarchy)
                    textWarning.text = "Anda sekarang berada di jalur T, anda harus kembali ke tempat semula";
                else if (arrowE.activeInHierarchy)
                    textWarning.text = "Anda sekarang berada di jalur E, anda harus kembali ke tempat semula";
                warningInfo.SetActive(true);
            }
            else
                arrowL.SetActive(true);
        }
        else
        {
            warningInfo.SetActive(false);
            arrowL.SetActive(false);
            arrowA.SetActive(false);
            arrowQ.SetActive(false);
            arrowH.SetActive(false);
        }
    }
    public void OnAndOffArrowA()
    {
        if (!arrowA.activeInHierarchy)
        {
            if (!arrowL.activeInHierarchy)
            {
                textWarning.text = "Anda harus melewati jalur L terlebih dahulu";
                warningInfo.SetActive(true);
            }
            else
                arrowA.SetActive(true);
        }
        else
        {
            arrowA.SetActive(false);
            arrowQ.SetActive(false);
            arrowH.SetActive(false);
        }
    }
    public void OnAndOffArrowQ()
    {
        if (!arrowQ.activeInHierarchy)
        {
            if (!arrowA.activeInHierarchy)
            {
                textWarning.text = "Anda harus melewati jalur A terlebih dahulu";
                warningInfo.SetActive(true);
            }
            else
                arrowQ.SetActive(true);
        }
        else
        {
            arrowQ.SetActive(false);
            arrowH.SetActive(false);
        }
    }
    public void OnAndOffArrowH()
    {
        if (!arrowH.activeInHierarchy)
        {
            if (!arrowQ.activeInHierarchy)
            {
                textWarning.text = "Anda harus melewati jalur Q terlebih dahulu";
                warningInfo.SetActive(true);
            }
            else
                arrowH.SetActive(true);
        }
        else
            arrowH.SetActive(false);
    }

    public void Submit()
    {
        if (arrowL.activeInHierarchy && arrowA.activeInHierarchy &&
            arrowQ.activeInHierarchy && arrowH.activeInHierarchy)
            Debug.Log("Anda benar");
        else
        {
            Debug.Log("Salah Jalur");
            ResetArrow();
        }
    }

    public void ResetArrow()
    {
        warningInfo.SetActive(false);
        arrowE.SetActive(false);
        arrowV.SetActive(false);
        arrowT.SetActive(false);
        arrowP.SetActive(false);
        arrowL.SetActive(false);
        arrowA.SetActive(false);
        arrowQ.SetActive(false);
        arrowH.SetActive(false);
    }
}
