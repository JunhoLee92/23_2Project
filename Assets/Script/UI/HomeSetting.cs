using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSetting : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject panel;

    public void TogglePause()
    {
      

        if(panel.activeSelf)
        {
            panel.SetActive(false);
         

            
        }
        else
        {
            panel.SetActive(true);
           

       
        }
    }
}
