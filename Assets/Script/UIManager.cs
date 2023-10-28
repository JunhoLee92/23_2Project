using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    

    public Text usernameText;
    public Text levelText;
    public Slider expBar;
    public float experience;
    public Image userImage;
    public Image profileUserImage;
    public GameObject popupPanel;
    private static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UpdateUI();
        
    }

    void Update()
    {
        UpdateUI();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePopupPanel();
        }
    }

    public void UpdateUI()
    {
        usernameText.text = "이준호";
        levelText.text = "99";
        expBar.value = (experience/100.0f);
        userImage.sprite = profileUserImage.sprite;
        userImage.color=profileUserImage.color;
    }

    public void TogglePopupPanel()
    {
        if (popupPanel.activeSelf)
        {
            popupPanel.SetActive(false);
            Time.timeScale = 1; // 게임 재개
        }
        else
        {
            popupPanel.SetActive(true);
            Time.timeScale = 0; // 게임 일시 정지
        }
    }
}







