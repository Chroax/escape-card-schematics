using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void DeactivePanel()
    {
        if(gameObject.activeInHierarchy)
            this.gameObject.SetActive(false);
    }
}
