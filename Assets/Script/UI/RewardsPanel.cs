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

    public Image Image1;
    public Image Image2;
    public Image Image3;

    public Sprite A;
    public Sprite B;  //...

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
        SetupRewardButton(reward1, rewardText1, rewards[0]);
        SetupRewardButton(reward2, rewardText2, rewards[1]);
        SetupRewardButton(reward3, rewardText3, rewards[2]);

        // gameObject.SetActive(true); /
        Debug.Log("show rewards");;
    }

     private void SetupRewardButton(Button button, Text buttonText, RewardCard reward)
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
