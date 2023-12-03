using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System;
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
      public static RoundRewardSystem Instance { get; private set; }
    public enum CardGrade { Common, Rare, SR, SSR, Special }
    public List<RewardCard> availableCards;  // All available reward cards
   public HashSet<RewardCard> selectedSpecialCards = new HashSet<RewardCard>();
     private List<RewardCard> allCards = new List<RewardCard>();

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
       
    };
  void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        
    }
      void Start()
    {
        InitializeAllCards();
        
    }

void Update()
{
    if(Input.GetKeyDown(KeyCode.A))
    {
        // TestSpecial();
        Debug.Log("Kali ON");
    } 
}


    private void InitializeAllCards()
    {
        string[] unitNames = { "Kali", "Yuki", "Lily", "Air", "Terres", "Bella" }; // Replace with actual unit names

        foreach (var unitName in unitNames)
        {
            allCards.Add(CreateCard(unitName, "공격력", 2, CardGrade.Common, () => AttackDamageEffect(unitName)));
            allCards.Add(CreateCard(unitName, "공격력", 5, CardGrade.Rare, () => AttackDamageEffect(unitName)));
            allCards.Add(CreateCard(unitName, "공격력", 11, CardGrade.SR, () => AttackDamageEffect(unitName)));
            allCards.Add(CreateCard(unitName, "공격속도", 5, CardGrade.Common, () => AttackSpeedEffect(unitName)));
            allCards.Add(CreateCard(unitName, "공격속도", 10, CardGrade.Rare, () => AttackSpeedEffect(unitName)));
            allCards.Add(CreateCard(unitName, "공격속도", 30, CardGrade.SR, () => AttackSpeedEffect(unitName)));
            
            // Add additional cards for other effects or grades as needed
        }
        allCards.Add(CreateCard("Kali", "공격범위", 20, CardGrade.Special, () => KaliSpecialA()));
        allCards.Add(CreateCard("Kali", "공격범위", 30, CardGrade.Special, () => KaliSpecialB()));
        allCards.Add(CreateCard("Yuki", "냉동확률", 15, CardGrade.Special, () => YukiSpecialA()));
        allCards.Add(CreateCard("Yuki", "둔화지속", 0.2f, CardGrade.Special, () => YukiSpecialB()));
        allCards.Add(CreateCard("Lily", "도트데미지", 5, CardGrade.Special, () => LilySpecialA(),"LV3이상 릴리의 도트 데미지 +5% 증가"));
        allCards.Add(CreateCard("Lily", "도트데미지 추가증가", 5, CardGrade.Special, () => LilySpecialB(),"LV5 릴리의 독 디버프 데미지 +10% 증가 및 최대 중첩수 +2 증가") );
        allCards.Add(CreateCard("Terres", "불씨강화", 30, CardGrade.Special, () => TerresSpecialB(),"LV3 이상 테레스의 불씨 1중첩 이상일 때 30% 확률로 불씨 1중첩을 소모하여 강공격을 한다. 강공격은 현재 테레스 공격력의 130% 데미지"));
        allCards.Add(CreateCard("Terres", "중첩강화", 5, CardGrade.Special, () => TerresSpecialB(),"LV5 테레스의 불씨 중첩 획득 확률 +5% 증가"));
        allCards.Add(CreateCard("Bella", "처형강화", 5, CardGrade.Special, () => BellaSpecialA(),"LV3 이상 벨라가 공격 시 적의 체력과 무관하게 5% 확률로 처형"));
        allCards.Add(CreateCard("Bella", "처형강화2", 45, CardGrade.Special, () => BellaSpecialB(),"LV5 이상 벨라의 즉시 처형 기준 체력 +15% 증가"));

        allCards.Add(CreateCard("몬스터", "속도감소", 5, CardGrade.Common, DecreaseEnemySpeed1, "적의 이동속도가 5% 감소합니다."));
        allCards.Add(CreateCard("몬스터", "속도감소", 10, CardGrade.SR, DecreaseEnemySpeed1, "적의 이동속도가 10% 감소합니다."));
        allCards.Add(CreateCard("몬스터", "속도감소", 15, CardGrade.SSR, DecreaseEnemySpeed1, "적의 이동속도가 15% 감소합니다."));
        allCards.Add(CreateCard("모든유닛", "공격속도", 10, CardGrade.SSR, IncreaseAttackDamage1, "유닛 전체의 공격 속도가 10% 증가합니다."));
         

    }

   
    private RewardCard CreateCard(string unitName, string effectType, float effectValue, CardGrade grade, Action effectAction, string customDescription = null)
{
    string description = customDescription;
    if (string.IsNullOrEmpty(customDescription))
    {
        description = effectType switch
        {
            "YukiSpecialA" => $"{unitName}의 둔화 지속 시간이 {effectValue}초 증가합니다.",
            // Add more cases for unique descriptions
            _ => $"{unitName}의 {effectType}이  {effectValue}% 증가합니다."
        };
    }

    return new RewardCard
    {
        Grade = grade,
        Name = $"{grade} {effectType} Card",
        EffectDescription = description,
        RelatedUnit = unitName,
        Effect = effectAction
    };
}

     
private CardGrade ParseCardGrade(string grade)
{
    switch (grade)
    {
        case "Common": return CardGrade.Common;
        case "Rare": return CardGrade.Rare;
        case "SR" : return CardGrade.SR;
        case "SSR" : return CardGrade.SSR;
        case "Special" : return CardGrade.Special;
        default: return CardGrade.Common; //default
    }
}
   public List<RewardCard> GenerateRoundRewards(int currentRound, List<string> activeUnits)
{
    List<RewardCard> selectedCards = new List<RewardCard>();
    List<RewardCard> possibleCards = FilterCardsBasedOnActiveUnits(activeUnits);
     possibleCards = possibleCards.Where(card => !selectedSpecialCards.Contains(card)).ToList(); 
    Debug.Log("activeUnits"+activeUnits);
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
        float randomValue = UnityEngine.Random.value * 100f;
        CardGrade selectedGrade = DetermineCardGrade(randomValue, round);
        return possibleCards.Where(card => card.Grade == selectedGrade).OrderBy(_ => UnityEngine.Random.value).FirstOrDefault();
    }

     public void OnSpecialCardSelected(RewardCard specialCard)
    {
        if (specialCard.Grade == CardGrade.Special)
        {
            selectedSpecialCards.Add(specialCard);
            Debug.Log("SpecialCardSelected");
        }
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
    return allCards.FindAll(card => 
        activeUnits.Contains(card.RelatedUnit) || 
        card.RelatedUnit == "모든유닛" || 
        card.RelatedUnit == "몬스터"
    ).ToList();
}



/// Skill Effect Bellow

public void KaliSpecialA()
{
      
        UnitAttack.isSpecialA=true;
        Debug.Log("스킬발동");
}

public void  KaliSpecialB()
{
      
        UnitAttack.isSpecialB=true;
        Debug.Log("스킬발동");
}

public void YukiSpecialA()
{
      
       YukiAttack.isSpecialA=true;
        Debug.Log("유키스킬발동");
}

public void YukiSpecialB()
{
      
        YukiAttack.isSpecialB=true;
        Debug.Log("유키스킬발동2");
}

public void LilySpecialA()
{
    LilyAttack.isSpecialA=true;
Debug.Log("LilySkillActive");
}
public void LilySpecialB()
{
    LilyAttack.isSpecialB=true;
    Debug.Log("LilySkillActive2");
}

public void AirSpecialA()
{
    
    Debug.Log("스킬발동");
}
public void AirSpecialB()
{
    Debug.Log("스킬발동");
}

public void TerresSpecialA()
{
    TeresAttack.isSpecialA=true;
    Debug.Log("테레스스킬발동");
}
public void TerresSpecialB()
{
    TeresAttack.isSpecialB=true;
    Debug.Log("테레스스킬발동2");
}

public void BellaSpecialA()
{
    BellaAttack.isSpecialA=true;
    Debug.Log("벨라스킬발동");
}

public void BellaSpecialB()
{
    BellaAttack.isSpecialB=true;
    Debug.Log("벨라스킬발동2");
}
public void DecreaseEnemySpeed1()
{
    Debug.Log("스킬발동");
}

public void IncreaseAttackDamage1()
{
Debug.Log("스킬발동");
}
public void AttackSpeedEffect(string unitName)
{
Debug.Log("스킬발동");
}
public void AttackDamageEffect(string untName)
{
Debug.Log("스킬발동");
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

