using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PausePanelManager : MonoBehaviour
{
    public Button restart;
    public Button home;
    public Button play;

    public Image[] unitThumbnails;

     
   

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale=0;
        GameManager.Instance.isGamePaused=true;
        restart.onClick.AddListener(() => ButtonClicked(restart));
        home.onClick.AddListener(() => ButtonClicked(home));
        if (play != null)
        {
            play.onClick.AddListener(() => ButtonClicked(play));
        }

        SquadUpdate();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SquadUpdate()
    {
       for (int i = 0; i < unitThumbnails.Length; i++)
        {
            if (i < Squad_Data.Instance.selectedUnitNames.Count)
            {
                string unitName = Squad_Data.Instance.selectedUnitNames[i];
                
                
                    var unitData = Array.Find(Squad_Data.Instance.unitEvolutionData, u => u.unitName == unitName);
                    if (unitData != null)
                    {
                        unitThumbnails[i].sprite = unitData.thumbnailSprite;
                        unitThumbnails[i].color = new Color(unitThumbnails[i].color.r, unitThumbnails[i].color.g, unitThumbnails[i].color.b, 1);
                        
                    }
                
               
            }
        }


       
    }
    private void ButtonClicked(Button button)
    {
        if (button == restart)
        {
            Scene currentScene = SceneManager.GetActiveScene();

            Time.timeScale = 1; if(GameManager.Instance!=null)
            {

            GameManager.Instance.isGamePaused = false;
            
            }
              SceneManager.LoadScene(currentScene.name);
        }

        else if (button == home)
        {
            Time.timeScale = 1; if(GameManager.Instance!=null)
            {

            GameManager.Instance.isGamePaused = false;
            
            }
            SceneManager.LoadScene("HomeScene");

        }
        else if (button == play)
        {
            Time.timeScale = 1;
             if(GameManager.Instance!=null)
            {

            GameManager.Instance.isGamePaused = false;
            
            }
            this.gameObject.SetActive(false);
          
        }
    }
}


