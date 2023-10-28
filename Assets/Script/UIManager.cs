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
    private void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        usernameText.text = "¿Ã¡ÿ»£";
        levelText.text = "99";
        expBar.value = (experience/100.0f);
        userImage.sprite = profileUserImage.sprite;
        userImage.color=profileUserImage.color;
    }
}
