using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButton : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    // Start is called before the first frame update
    public void ActivatePanel()
    {
        if(!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
    }
}
