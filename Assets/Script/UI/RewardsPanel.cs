using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RewardsPanel : MonoBehaviour
{
    public Button reward1;
    public Button reward2;
    public Button reward3;

    public Text rewardText1; 
    public Text rewardText2; 
    public Text rewardText3; 

    public Sprite commonCardImage;
public Sprite rareCardImage;
public Sprite srCardImage;
public Sprite ssrCardImage;
public Sprite specialCardImage;

    public Image image1;
    public Image image2;
    public Image image3;

    public Sprite attackSpeedImage;
    public Sprite attackDamageImage;

    public Sprite allAttackSpeedImage;
    
    public Sprite allAttackDamageImage;

    public Sprite  enemySpeedImage;

    public Sprite kaliSpecial1Image;

    public Sprite kaliSpecial2Image;

    public Sprite lilySpecial1Image;

    public Sprite lilySpecial2Image;

    public Sprite yukiSpecial1Image;

    public Sprite yukiSpecial2Image;

    public Sprite terresSpecial1Image;

    public Sprite terresSpecial2Image;

    public Sprite bellaSpecial1Image;

    public Sprite bellaSpecial2Image;
      //...

    private List<RewardCard> currentRewards;
    void Start()
    {
       

    }

      public void ShowRewards(List<RewardCard> rewards)
    {
        currentRewards = rewards;
   if (rewards == null || rewards.Count < 3)
    {
        Debug.LogError("Rewards list is empty, null, or insufficient.");
        return;
    }
        SetupRewardButton(reward1, rewardText1,image1, rewards[0]);
        SetupRewardButton(reward2, rewardText2,image2, rewards[1]);
        SetupRewardButton(reward3, rewardText3,image3, rewards[2]);

        // gameObject.SetActive(true); /
        Debug.Log("show rewards");;
    }

     private void SetupRewardButton(Button button, Text buttonText,Image image, RewardCard reward)
    {
        Debug.Log("settingUp");

         Image buttonImage = button.GetComponent<Image>();

          switch (reward.Grade)
    {
        case RoundRewardSystem.CardGrade.Common:
            buttonImage.sprite = commonCardImage;
            break;
        case RoundRewardSystem.CardGrade.Rare:
            buttonImage.sprite = rareCardImage;
            break;
        case RoundRewardSystem.CardGrade.SR:
            buttonImage.sprite = srCardImage;
            break;
        case RoundRewardSystem.CardGrade.SSR:
            buttonImage.sprite = ssrCardImage;
            break;
        case RoundRewardSystem.CardGrade.Special:
            buttonImage.sprite = specialCardImage;
            break;

    }

    switch (reward.imageID)   //Image Change
    {
        case 1:
        image.sprite=attackSpeedImage;
        break;

        case 2:
         image.sprite=attackDamageImage;
        break;

        case 3:
        image.sprite=allAttackSpeedImage;
        break;

        case 4:
        image.sprite=allAttackDamageImage;
        break;

        case 5:
        image.sprite=enemySpeedImage;
        break;

        case 6:
        image.sprite=kaliSpecial1Image;
        break;

        case 7:
        image.sprite=kaliSpecial2Image;
        break;

        case 8:
        image.sprite=lilySpecial1Image;
        break;

        case 9:
        image.sprite=lilySpecial2Image;
        break;

        case 10:
       image.sprite=yukiSpecial1Image;
        break;

        case 11:
        image.sprite=yukiSpecial2Image;
        break;

        case 12:
        image.sprite=terresSpecial1Image;
        break;

        case 13:
        image.sprite=terresSpecial2Image;
        break;

        case 14:
        image.sprite=bellaSpecial1Image;
        break;

        case 15:
       image.sprite= bellaSpecial2Image;
        break;
    }

        if (button == null || buttonText == null || reward == null)
        {
        Debug.LogError("Button, text, or Reward null.");
        return;
        }
        buttonText.text = reward.EffectDescription; 
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => ApplyReward(reward));
        Debug.Log("Setup Rewards");
    }
    private void ApplyReward(RewardCard reward)
    { Debug.Log("Reward Applied: " + reward.Name);
        reward.Effect(); 
        if (reward.Grade == RoundRewardSystem.CardGrade.Special)
    {
        RoundRewardSystem.Instance.OnSpecialCardSelected(reward);
        Debug.Log("SpecialCard"+reward);
    }
        Time.timeScale=1;
        GameManager.Instance.isGamePaused = false;
        
        gameObject.SetActive(false); 
        
    }
   
}
