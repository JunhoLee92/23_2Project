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
        restart.onClick.AddListener(() => ButtonClicked(restart));
        home.onClick.AddListener(() => ButtonClicked(home));
        play.onClick.AddListener(() => ButtonClicked(play));


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ButtonClicked(Button button)
    {
        if (button == restart)
        {
            SceneManager.LoadScene("Ingame");
        }
        else if(button == home)
        {
            SceneManager.LoadScene("HomeScene");

        }
        else if (button == play)
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}


