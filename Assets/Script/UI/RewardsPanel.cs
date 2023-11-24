using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RewardsPanel : MonoBehaviour
{
    public Button reward1;
    public Button reward2;
    public Button reward3;
    void Start()
    {
        reward1.onClick.AddListener(() => ButtonClicked(reward1));
       reward2.onClick.AddListener(() => ButtonClicked(reward2));
        reward3.onClick.AddListener(() => ButtonClicked(reward3));
       

    }
    // Start is called before the first frame update
    public void ButtonClicked(Button button)
    {
        if (button == reward1)
        {
            Time.timeScale = 1;
            GameManager.Instance.isGamePaused = false;
            this.gameObject.SetActive(false);
            
        }

        else if (button == reward2)
        {
            Time.timeScale = 1;
            GameManager.Instance.isGamePaused = false;
            this.gameObject.SetActive(false);


        }
        else if (button == reward3)
        {
            Time.timeScale = 1;
            GameManager.Instance.isGamePaused = false;
            this.gameObject.SetActive(false);


        }
    }
}
