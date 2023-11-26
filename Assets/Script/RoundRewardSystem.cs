using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
[System.Serializable]
public class RewardCardTemplate
{
    public string Grade;
    public string EffectType;
    public float EffectValue;
}

[System.Serializable]
public class RewardCardTemplateList
{
    public List<RewardCardTemplate> templates;
}
public class RoundRewardSystem : MonoBehaviour
{
    
    public enum CardGrade { Common, Rare, SR, SSR, Special }
    public List<RewardCard> availableCards;  // All available reward cards
    private List<RewardCard> selectedSpecialCards = new List<RewardCard>(); // Track selected special cards
     private List<RewardCardTemplate> cardTemplates;
    private Dictionary<int, Dictionary<CardGrade, float>> roundProbabilities = new Dictionary<int, Dictionary<CardGrade, float>>
    {
        {0, new Dictionary<CardGrade, float> {{CardGrade.Common, 80.38f}, {CardGrade.Rare, 8.78f}, {CardGrade.SR, 0.56f}, {CardGrade.SSR, 0.28f}, {CardGrade.Special, 10f}}},
         {1, new Dictionary<CardGrade, float> {{CardGrade.Common, 67.47f}, {CardGrade.Rare, 15.05f}, {CardGrade.SR, 1.67f}, {CardGrade.SSR, 0.81f}, {CardGrade.Special, 15f}}},
          {2, new Dictionary<CardGrade, float> {{CardGrade.Common, 54.56f}, {CardGrade.Rare, 21.32f}, {CardGrade.SR, 2.78f}, {CardGrade.SSR, 1.34f}, {CardGrade.Special, 20f}}},
           {3, new Dictionary<CardGrade, float> {{CardGrade.Common, 41.65f}, {CardGrade.Rare, 27.59f}, {CardGrade.SR, 3.89f}, {CardGrade.SSR, 1.87f}, {CardGrade.Special, 25f}}},
            {4, new Dictionary<CardGrade, float> {{CardGrade.Common, 28.74f}, {CardGrade.Rare, 33.85f}, {CardGrade.SR, 5f}, {CardGrade.SSR, 2.4f}, {CardGrade.Special, 30f}}},
            {5, new Dictionary<CardGrade, float> {{CardGrade.Common, 14.83f}, {CardGrade.Rare, 37.32f}, {CardGrade.SR, 9.27f}, {CardGrade.SSR, 3.58f}, {CardGrade.Special, 35f}}},
            {6, new Dictionary<CardGrade, float> {{CardGrade.Common, 0f}, {CardGrade.Rare,42.78f}, {CardGrade.SR, 12.46f}, {CardGrade.SSR, 4.76f}, {CardGrade.Special, 40f}}},
            {7, new Dictionary<CardGrade, float> {{CardGrade.Common, 0f}, {CardGrade.Rare, 33.41f}, {CardGrade.SR, 15.65f}, {CardGrade.SSR, 5.94f}, {CardGrade.Special, 45f}}},
            {8, new Dictionary<CardGrade, float> {{CardGrade.Common, 0f}, {CardGrade.Rare, 24.04f}, {CardGrade.SR, 18.84f}, {CardGrade.SSR, 7.12f}, {CardGrade.Special, 50f}}}
        // ... Add probabilities for other rounds
    };

      void Start()
    {
        LoadCardDataFromJson();
        
    }
//    private void LoadCardDataFromJson()
//     {
//         string filePath = Path.Combine(Application.streamingAssetsPath, "Reward.json");
//         if (File.Exists(filePath))
//         {
//             string dataAsJson = File.ReadAllText(filePath);
//             RewardCardTemplateList loadedData = JsonUtility.FromJson<RewardCardTemplateList>("{\"templates\":" + dataAsJson + "}");
//             cardTemplates = loadedData.templates;
//         }
//         else
//         {
//             Debug.LogError("Cannot Find Card Data");
//         }
//     }

private void LoadCardDataFromJson()
{
    string filePath = Path.Combine(Application.streamingAssetsPath, "Reward.json");
    if (File.Exists(filePath))
    {
        string dataAsJson = File.ReadAllText(filePath);
        RewardCardTemplateList loadedData = JsonUtility.FromJson<RewardCardTemplateList>("{\"templates\":" + dataAsJson + "}");
        cardTemplates = loadedData.templates;

         availableCards.Clear(); // 
    foreach (var template in cardTemplates)
    {
        RewardCard newCard = new RewardCard
        {
            Grade = ParseCardGrade(template.Grade),
            Name = template.Grade + " Attack Power Card",
            EffectDescription = "Increases attack power by " + template.EffectValue,
            RelatedUnit="Kali"
            // need Effect Delegate Setting 
        };
        availableCards.Add(newCard);
    }
    }
    else
    {
        Debug.LogError("Cannot Find Card Data");
    }
}

private CardGrade ParseCardGrade(string grade)
{
    switch (grade)
    {
        case "Common": return CardGrade.Common;
        case "Rare": return CardGrade.Rare;
        case "SR" : return CardGrade.SR;
        case "SSR" : return CardGrade.SSR;
        default: return CardGrade.Common; //default
    }
}
   public List<RewardCard> GenerateRoundRewards(int currentRound, List<string> activeUnits)
{
    List<RewardCard> selectedCards = new List<RewardCard>();
    List<RewardCard> possibleCards = FilterCardsBasedOnActiveUnits(activeUnits);

    for (int i = 0; i < 3; i++)
    {
        RewardCard selectedCard = SelectCardForRound(currentRound, possibleCards);
        if (selectedCard != null)
        {
            selectedCards.Add(selectedCard);
        }
        else
        {
            Debug.LogError("Cannot Find available Cards.");
        }
    }

    return selectedCards;
}
    

    private RewardCard SelectCardForRound(int round, List<RewardCard> possibleCards)
    {
        float randomValue = Random.value * 100f;
        CardGrade selectedGrade = DetermineCardGrade(randomValue, round);
        return possibleCards.Where(card => card.Grade == selectedGrade).OrderBy(_ => Random.value).FirstOrDefault();
    }

    private CardGrade DetermineCardGrade(float randomValue, int round)
    {
        float accumulatedProbability = 0f;
        foreach (var gradeProbability in roundProbabilities[round])
        {
            accumulatedProbability += gradeProbability.Value;
            if (randomValue <= accumulatedProbability)
                return gradeProbability.Key;
        }
        return CardGrade.Common;  // Fallback
    }

    private List<RewardCard> FilterCardsBasedOnActiveUnits(List<string> activeUnits)
    {
        return availableCards.Where(card => activeUnits.Contains(card.RelatedUnit)).ToList();
    }

     
}

[System.Serializable]
public class RewardCard
{
    public RoundRewardSystem.CardGrade Grade;
    public string Name;
    public string RelatedUnit; 
    public string EffectDescription;
    public System.Action Effect;

    public void ApplyEffect()
    {
        
        Debug.Log($"{Name}Apply Effect");
    }
}