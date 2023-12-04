using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour
{
    public Button restart;
    public Button home;
    public Button play;
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ButtonClicked(Button button)
    {
        if (button == restart)
        {
            Time.timeScale = 1; if(GameManager.Instance!=null)
            {

            GameManager.Instance.isGamePaused = false;
            
            }
            SceneManager.LoadScene("Ingame");
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


