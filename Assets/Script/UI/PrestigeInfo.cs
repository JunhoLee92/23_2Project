using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prestigeinfo;
    public void OnButtonClick()
    {
        if (prestigeinfo.activeSelf == false)
        {
            prestigeinfo.SetActive(true);
        }
        else if (prestigeinfo.activeSelf == true)
        {
            prestigeinfo.SetActive(false);
        }

    }
}
