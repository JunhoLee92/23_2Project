using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class UnitEvolutionData
{
    public string unitName;
    public int maxLevel = 5;
    public GameObject[] unitPrefabs;
    public float baseAttackPower = 10f;
}

public class GameManager : MonoBehaviour
{
    public UnitEvolutionData[] unitEvolutionData; // Define this array in the inspector
    public Transform[] spawnPositions;
    private GameObject[] grid;
    private GameObject selectedUnit;
    private bool isFirstClick = true;
    public int movesPerRound ; // 예: 5회의 이동 횟수를 가진다고 가정
    private int currentMoves = 0;
    private MonsterSponer monsterSpawner;
    public Text movesText;
    [System.Serializable]
    public class RoundConfig
    {
        public int movesPerRound;
        public int monstersPerSpawn; //한번에 몇마리 생성할지
        
        public float spawnInterval;
        public List<MonsterSpawnInfo> spawnInfos;
        
    }

    [System.Serializable]
    public class MonsterSpawnInfo
    {
        public MonsterType monsterType;
        public int count;
        public float Hp;
        public float speed;
    }

    [System.Serializable]
    public class ChapterConfig
    {
        public RoundConfig[] rounds = new RoundConfig[10];
    }
    public ChapterConfig[] chapters;
    private int currentChapter = 0;  // Current chapter index
    private int currentRound = 0;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "GameManager";
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }



    void Start()
    {
       
        monsterSpawner = FindObjectOfType<MonsterSponer>();

        grid = new GameObject[spawnPositions.Length];
        InitGrid();

        UpdateMovesText();

        SetupRound();
    }

    public RoundConfig GetCurrentRoundConfig()
    {
        return chapters[currentChapter].rounds[currentRound];
    }
    public void IncreaseUnitAttackPowerByPercentage(string unitName, float percentage)
    {
        foreach (UnitEvolutionData unitData in unitEvolutionData)
        {
            if (unitData.unitName == unitName)
            {
                unitData.baseAttackPower += unitData.baseAttackPower * (percentage / 100f);
                break;
            }
        }
    }
    void SetupRound()
    {
        Debug.Log("Setting up round: " + currentRound);
        monsterSpawner.totalSpawnedCount = 0;  // 라운드가 시작될 때 몬스터 소환 카운트를 0으로 초기화합니다.
        monsterSpawner.spawnStarted = false;
        if (currentRound < chapters[currentChapter].rounds.Length)
        {
            movesPerRound = chapters[currentChapter].rounds[currentRound].movesPerRound;
            monsterSpawner.monstersPerSpawn = chapters[currentChapter].rounds[currentRound].monstersPerSpawn;
            monsterSpawner.spawnInterval = chapters[currentChapter].rounds[currentRound].spawnInterval;
            currentMoves = 0;
            UpdateMovesText();
        }
        else
        {
            // 게임 종료 또는 다른 로직
        }
        
    }

    //public void OnMonsterSpawned()
    //{
    //    Debug.Log("OnMonsterSpawned called");

    //    int totalMonstersForThisRound = 0;
    //    foreach (MonsterSpawnInfo info in chapters[currentChapter].rounds[currentRound].spawnInfos)
    //    {
    //        totalMonstersForThisRound += info.count;
    //    }

    //    if (--totalMonstersForThisRound <= 0)
    //    {
    //        monsterSpawner.spawnStarted = false; // 스폰 중지

    //        currentRound++; // 다음 라운드로

    //        if (currentRound >= chapters[currentChapter].rounds.Length)  // Check if all rounds in the current chapter are completed
    //        {
    //            if (currentChapter < chapters.Length - 1)  // Ensure we're not exceeding the total number of chapters
    //            {
    //                currentChapter++;  // Move to the next chapter
    //                currentRound = 0;  // Reset the round to the first round of the new chapter
    //            }
    //            else
    //            {
    //                // Handle the end of the last chapter, if necessary
    //            }
    //        }
    //        SetupRound(); // 라운드 설정
    //    }
    //}

    public void OnMonsterSpawned()
    {
        Debug.Log("OnMonsterSpawned called");

        int totalMonstersForThisRound = 0;
        foreach (MonsterSpawnInfo info in chapters[currentChapter].rounds[currentRound].spawnInfos)
        {
            totalMonstersForThisRound += info.count;
        }

        if (monsterSpawner.totalSpawnedCount >= totalMonstersForThisRound)
        {
            monsterSpawner.spawnStarted = false;  // 스폰 중지

            currentRound++;  // 다음 라운드로

            if (currentRound >= chapters[currentChapter].rounds.Length)  // Check if all rounds in the current chapter are completed
            {
                if (currentChapter < chapters.Length - 1)  // Ensure we're not exceeding the total number of chapters
                {
                    currentChapter++;  // Move to the next chapter
                    currentRound = 0;  // Reset the round to the first round of the new chapter
                }
                else
                {
                    // Handle the end of the last chapter, if necessary
                }
            }
            SetupRound();  // 라운드 설정
        }
    }
    void UpdateMovesText()
    {
        movesText.text = (movesPerRound - currentMoves).ToString();
    }
    void InitGrid()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            int randomUnitType = Random.Range(0, unitEvolutionData.Length);
            int initialLevel = 0; // Start with level 0 units
            GameObject unitPrefab = unitEvolutionData[randomUnitType].unitPrefabs[initialLevel];
            GameObject unit = Instantiate(unitPrefab, spawnPositions[i].position, Quaternion.identity);
            unit.GetComponent<Unit>().unitType = randomUnitType;
            unit.GetComponent<Unit>().unitLevel = initialLevel;
            unit.GetComponent<Unit>().SetGridPosition(i);
            grid[i] = unit;
        }
    }


    public void OnUnitClicked(GameObject unit)
    {
        if (isFirstClick)
        {
            selectedUnit = unit;
            isFirstClick = false;
            // Highlight selected units
            // selectedUnit.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            if (selectedUnit == unit)
            {
                selectedUnit = null;
                isFirstClick = true;
                unit.GetComponent<SpriteRenderer>().color = Color.white;

                // Create a new level 0 unit of the same type in the position of the first clicked unit
                int newUnitType = selectedUnit.GetComponent<Unit>().unitType;
                int newUnitLevel = 0; // Always create a level 0 unit
                GameObject newUnitPrefab = unitEvolutionData[newUnitType].unitPrefabs[newUnitLevel];
                GameObject newUnit = Instantiate(newUnitPrefab, selectedUnit.transform.position, Quaternion.identity);
                newUnit.GetComponent<Unit>().unitType = newUnitType; // Set the correct unit type
                newUnit.GetComponent<Unit>().SetGridPosition(selectedUnit.GetComponent<Unit>().gridIndex);
                return;
            }

            // Calculate the distance between the two units
            float distance = Vector3.Distance(selectedUnit.transform.position, unit.transform.position);

            if (distance < 1.5f) // Adjust this threshold as needed
            {
                if (selectedUnit.GetComponent<Unit>().unitType == unit.GetComponent<Unit>().unitType &&
                    selectedUnit.GetComponent<Unit>().unitLevel == unit.GetComponent<Unit>().unitLevel)
                {
                    // Get the current level of the second unit
                    int currentLevel = unit.GetComponent<Unit>().unitLevel;

                    // Ensure the new level doesn't exceed the maximum level
                    int newLevel = currentLevel + 1;
                    if (newLevel <= unitEvolutionData[unit.GetComponent<Unit>().unitType].maxLevel)
                    {
                        // Instantiate the new unit prefab for the second unit
                        GameObject newUnitPrefab = unitEvolutionData[unit.GetComponent<Unit>().unitType].unitPrefabs[newLevel];
                        GameObject newUnit = Instantiate(newUnitPrefab, unit.transform.position, Quaternion.identity);
                        newUnit.GetComponent<Unit>().unitType = unit.GetComponent<Unit>().unitType; // Set the correct unit type
                        newUnit.GetComponent<Unit>().SetGridPosition(unit.GetComponent<Unit>().gridIndex);
                        newUnit.GetComponent<Unit>().unitLevel = newLevel; // Set the new level

                        // Destroy the second unit
                        Destroy(unit);
                    }

                    // Create a random level 0 unit at the location of the first unit
                    int randomUnitType = Random.Range(0, unitEvolutionData.Length); // Choose a random unit type
                    GameObject randomUnitPrefab = unitEvolutionData[randomUnitType].unitPrefabs[0]; // Level 0 prefab
                    GameObject newRandomUnit = Instantiate(randomUnitPrefab, selectedUnit.transform.position, Quaternion.identity);
                    newRandomUnit.GetComponent<Unit>().unitType = randomUnitType; // Set the correct unit type
                    newRandomUnit.GetComponent<Unit>().SetGridPosition(selectedUnit.GetComponent<Unit>().gridIndex);

                    // Destroy the first unit
                    Destroy(selectedUnit);
                }
                else
                {
                    // Swap positions of the two units
                    SwapUnits(selectedUnit, unit);
                }

                currentMoves++;
                UpdateMovesText();
                if (currentMoves >= movesPerRound)
                {
                    monsterSpawner.spawnStarted = true;
                    currentMoves = 0; // 이동 횟수를 리셋

                }
            }

            selectedUnit = null;
            isFirstClick = true;
        }

        // 유닛이 움직이거나 진화했을 때

        void SwapUnits(GameObject unit1, GameObject unit2)
        {
            Vector3 tempPosition = unit1.transform.position;
            unit1.transform.position = unit2.transform.position;
            unit2.transform.position = tempPosition;
        }
    }

}