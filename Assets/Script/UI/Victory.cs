using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject victory;
    public GameObject defaeat;

public Canvas uiCanvas; // Assign your Canvas in the Inspector

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

    

}

public void DefeatOn()
{
    DisableUIExceptVictory();
    defaeat.SetActive(true);
    
}

}
