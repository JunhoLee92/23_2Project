using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectButton : MonoBehaviour
{
    // Start is called before the first frame update
  public GameObject effect;
    public void OnButtonClick()
    {
        if (effect.activeSelf==false) 
        {
            effect.SetActive(true);
        }
        else if (effect.activeSelf==true)
        {
            effect.SetActive(false);
        }

    }
}
