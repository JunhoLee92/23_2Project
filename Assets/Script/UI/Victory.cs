using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public GameObject victory;
    public GameObject defaeat;
    public GameObject backgroundOverlay;
    public Canvas uiCanvas;


    public void DisableUIExceptVictory()
{
    foreach (Transform child in uiCanvas.transform)
    {
       
            child.gameObject.SetActive(false);
       
    }
}

public void VictoryOn()
{
    DisableUIExceptVictory();
    victory.SetActive(true);
    backgroundOverlay.SetActive(true);
    

}

public void DefeatOn()
{
    DisableUIExceptVictory();
    defaeat.SetActive(true);
    backgroundOverlay.SetActive(true);
    
}

}
