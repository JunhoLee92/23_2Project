using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class RewardCard
{
    public enum CardGrade { Common, Rare, SR, SSR, Special }

    public CardGrade grade;
    public string cardName;
    public string effectDescription;
    public System.Action effect;  // Delegate to represent the effect function to be executed
}

public class RoundRewardSystem : MonoBehaviour
{
    private RewardCard selectedCard;
    public List<RewardCard> availableCards;  // All available reward cards
    public List<RewardCard> selectedCards = new List<RewardCard>();  // The 3 randomly selected cards for the round

    // Probabilities for each card grade for each round
    private Dictionary<int, Dictionary<RewardCard.CardGrade, float>> roundProbabilities = new Dictionary<int, Dictionary<RewardCard.CardGrade, float>>
    {
        // Example probabilities for the first two rounds (based on the provided data)
        {1, new Dictionary<RewardCard.CardGrade, float> {{RewardCard.CardGrade.Common, 80.38f}, {RewardCard.CardGrade.Rare, 8.78f}, {RewardCard.CardGrade.SR, 0.56f}, {RewardCard.CardGrade.SSR, 0.28f}, {RewardCard.CardGrade.Special, 10f}}},
        {2, new Dictionary<RewardCard.CardGrade, float> {{RewardCard.CardGrade.Common, 67.47f}, {RewardCard.CardGrade.Rare, 15.05f}, {RewardCard.CardGrade.SR, 1.67f}, {RewardCard.CardGrade.SSR, 0.81f}, {RewardCard.CardGrade.Special, 15f}}}
        // ... Add probabilities for other rounds
    };

    // Generate 3 random reward cards based on the round probabilities
    public void GenerateRoundRewards(int currentRound)
    {
        for (int i = 0; i < 3; i++)
        {
            float randomValue = Random.Range(0f, 100f);
            RewardCard.CardGrade selectedGrade = DetermineCardGrade(randomValue, currentRound);
            RewardCard generateCard = availableCards.FirstOrDefault(card => card.grade == selectedGrade);  // Get a card of the determined grade
            if (generateCard != null)
            {
                selectedCards.Add(generateCard);
            }
        }
    }

    // Determine the card grade based on the random value and round probabilities
    private RewardCard.CardGrade DetermineCardGrade(float randomValue, int currentRound)
    {
        // Use the probabilities for the current round to determine the card grade
        float accumulatedProbability = 0f;
        foreach (var pair in roundProbabilities[currentRound])
        {
            accumulatedProbability += pair.Value;
            if (randomValue <= accumulatedProbability)
            {
                return pair.Key;
            }
        }
        return RewardCard.CardGrade.Common;  // Default to Common grade if no match found
    }

    // Apply the effect of the selected card
    public void ApplyCardEffect(RewardCard selectedCard)
    {
        selectedCard.effect.Invoke();  // Execute the effect of the selected card
    }

    public void IncreaseUnitAttackPower(string unitName, float percentage)
    {
        GameManager.Instance.IncreaseUnitAttackPowerByPercentage(unitName, percentage);
    }

    public void DecreaseMonsterSpeed(float percentage)
    {
        // Logic to decrease monster's movement speed by the given percentage
        // This can be implemented once the Monster's speed management system is available
    }

    public void ApplySelectedCardEffect()
    {
        if (selectedCard != null)
        {
            selectedCard.effect.Invoke();  // Execute the effect of the selected card
        }
    }

}
