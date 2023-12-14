using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    
    public GameObject panel;
    public GameObject backgroundPanel;

    public void TogglePause()
    {
      

        if(panel.activeSelf)
        {
            panel.SetActive(false);
            backgroundPanel.SetActive(false);
            Time.timeScale = 1;

            if(GameManager.Instance!=null)
            {

            GameManager.Instance.isGamePaused = false;
            
            }
        }
        else
        {
            panel.SetActive(true);
            backgroundPanel.SetActive(true);
            Time.timeScale = 0;

          if(GameManager.Instance!=null)
            {

            GameManager.Instance.isGamePaused = false;
            }
        }
    }
}
