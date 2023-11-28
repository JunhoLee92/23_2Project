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

    private List<RewardCard> currentRewards;
    void Start()
    {
    //     reward1.onClick.AddListener(() => ButtonClicked(reward1));
    //    reward2.onClick.AddListener(() => ButtonClicked(reward2));
    //     reward3.onClick.AddListener(() => ButtonClicked(reward3));

    

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

        if (button == null || buttonText == null || reward == null)
        {
        Debug.LogError("Button, text, or Reward null.");
        return;
        }
        buttonText.text = reward.Name; 
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => ApplyReward(reward));
        Debug.Log("Setup Rewards");
    }
    private void ApplyReward(RewardCard reward)
    { Debug.Log("Reward Applied: " + reward.Name);
        reward.ApplyEffect(); 
        if (reward.Grade == RoundRewardSystem.CardGrade.Special)
    {
        RoundRewardSystem.Instance.OnSpecialCardSelected(reward);
        Debug.Log("SpecialCard"+reward);
    }
        Time.timeScale=1;
        GameManager.Instance.isGamePaused = false;
        
        gameObject.SetActive(false); 
        
    }
    // Start is called before the first frame update
    // public void ButtonClicked(Button button)
    // {
    //     if (button == reward1)
    //     {
    //         Time.timeScale = 1;
    //         GameManager.Instance.isGamePaused = false;
    //         this.gameObject.SetActive(false);
            
    //     }

    //     else if (button == reward2)
    //     {
    //         Time.timeScale = 1;
    //         GameManager.Instance.isGamePaused = false;
    //         this.gameObject.SetActive(false);


    //     }
    //     else if (button == reward3)
    //     {
    //         Time.timeScale = 1;
    //         GameManager.Instance.isGamePaused = false;
    //         this.gameObject.SetActive(false);


    //     }
    // }
}
