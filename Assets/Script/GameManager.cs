using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


//[System.Serializable]
//public class UnitEvolutionData
//{
//    public string unitName;
//    public int maxLevel = 5;
//    public GameObject[] unitPrefabs;
//    public bool isChecked; //배열 제외 여부
//}

public class GameManager : MonoBehaviour
{
    public UnitEvolutionData[] unitEvolutionData; // Define this array in the inspector
    private UnitEvolutionData[] filteredEvolutions; //FilteredEvolutonData
    public Transform[] spawnPositions;
    private GameObject[] grid;
    private GameObject selectedUnit;
    private bool isFirstClick = true;
    public int movesPerRound; // 예: 5회의 이동 횟수를 가진다고 가정
    private int currentMoves = 0;
    private MonsterSponer monsterSpawner;
    public Text movesText;
    public Text RoundText;
    private BossSpawner bossSpawner;
    private BossController bossController; //보스컨트롤러 
    public bool isMonsterSpawning = false;
    private int activeMonsters;
    [System.Serializable]
    public class RoundConfig
    {
        public int movesPerRound;
        public int monstersPerSpawn; //한번에 몇마리 생성할지
        public bool isBossRound;  // 이 라운드에 보스가 스폰되는지 여부
        public GameObject bossPrefab;

        public float spawnInterval;
        public List<MonsterSpawnInfo> spawnInfos;

    }

    [System.Serializable]
    public class MonsterSpawnInfo  //몬스터 스폰 정보
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
        //DontDestroyOnLoad(this.gameObject);

        if (Squad_Data.Instance != null)
        {
            unitEvolutionData = Squad_Data.Instance.unitEvolutionData;
        }
    }



    void Start()
    {

        monsterSpawner = FindObjectOfType<MonsterSponer>();
        bossSpawner = FindObjectOfType<BossSpawner>();
        bossController = FindObjectOfType<BossController>(); //보스컨트롤러
        grid = new GameObject[spawnPositions.Length];

        //유닛정보 필터넣기
        int count = 0;
        foreach (var evolution in unitEvolutionData)
        {
            if (evolution.isChecked)
            {
                count++;
            }
        }

        // 필터링된 배열을 새로 만들기
        filteredEvolutions = new UnitEvolutionData[count];
        int index = 0;
        foreach (var evolution in unitEvolutionData)
        {
            if (evolution.isChecked)
            {
                filteredEvolutions[index] = evolution;
                index++;
            }
        }

        InitGrid();

        UpdateMovesText();
        UpdateRoundsText();

        SetupRound();
    }


    public RoundConfig GetCurrentRoundConfig()
    {
        return chapters[currentChapter].rounds[currentRound];
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
   


    public void OnMonsterSpawned()
    {
        RoundConfig currentRoundConfig = chapters[currentChapter].rounds[currentRound];
        isMonsterSpawning = true;

        Debug.Log("OnMonsterSpawned called");

        int totalMonstersForThisRound = 0;
        foreach (MonsterSpawnInfo info in chapters[currentChapter].rounds[currentRound].spawnInfos)
        {
            
            totalMonstersForThisRound += info.count;
            activeMonsters = totalMonstersForThisRound;
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
            UpdateRoundsText(); //라운드 텍스트
            SetupRound();  // 라운드 설정
        }
    }

    public void OnMonsterDestroyed()
    {
        activeMonsters--;
        if (activeMonsters <= 0)
        {
            isMonsterSpawning = false;
        }
    }
    void UpdateMovesText()
    {
        movesText.text = (movesPerRound - currentMoves).ToString();
    }

    void UpdateRoundsText()
    {
        RoundText.text = (currentRound+1).ToString();
    }
    void InitGrid()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            int randomUnitType = Random.Range(0, filteredEvolutions.Length);
            int initialLevel = 0; // Start with level 0 units
            GameObject unitPrefab = filteredEvolutions[randomUnitType].unitPrefabs[initialLevel];
            GameObject unit = Instantiate(unitPrefab, spawnPositions[i].position, Quaternion.identity);
            unit.GetComponent<Unit>().unitType = randomUnitType;
            unit.GetComponent<Unit>().unitLevel = initialLevel;
            unit.GetComponent<Unit>().SetGridPosition(i);
            grid[i] = unit;
        }
    }


    public void OnUnitClicked(GameObject unit)
    {
        // 몬스터 스폰 중에는 아무런 동작도 수행하지 않음
        if (isMonsterSpawning)
        {
            return;
        }
        RoundConfig currentRoundConfig = chapters[currentChapter].rounds[currentRound];
        if (isFirstClick)
        {
            selectedUnit = unit;
            isFirstClick = false;

        }
        else
        {
            if (selectedUnit == unit)
            {
                selectedUnit = null;
                isFirstClick = true;
                
                
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
                    if (newLevel <= filteredEvolutions[unit.GetComponent<Unit>().unitType].maxLevel)
                    {
                        // Instantiate the new unit prefab for the second unit
                        GameObject newUnitPrefab = filteredEvolutions[unit.GetComponent<Unit>().unitType].unitPrefabs[newLevel];
                        GameObject newUnit = Instantiate(newUnitPrefab, unit.transform.position, Quaternion.identity);
                        newUnit.GetComponent<Unit>().unitType = unit.GetComponent<Unit>().unitType; // Set the correct unit type
                        newUnit.GetComponent<Unit>().SetGridPosition(unit.GetComponent<Unit>().gridIndex);
                        newUnit.GetComponent<Unit>().unitLevel = newLevel; // Set the new level

                        // Destroy the second unit
                        Destroy(unit);
                    }

                    // Create a random level 0 unit at the location of the first unit
                    int randomUnitType = Random.Range(0, filteredEvolutions.Length); // Choose a random unit type
                    GameObject randomUnitPrefab = filteredEvolutions[randomUnitType].unitPrefabs[0]; // Level 0 prefab
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
                    if (currentRoundConfig.isBossRound)
                    {
                        bossSpawner.SpawnBoss();
                    }
                    else
                    {
                        monsterSpawner.spawnStarted = true;
                    }
                    currentMoves = 0; // 이동 횟수를 리셋

                }
            }

            selectedUnit = null;
            isFirstClick = true;
        }



        void SwapUnits(GameObject unit1, GameObject unit2)
        {
            Vector3 tempPosition = unit1.transform.position;
            unit1.transform.position = unit2.transform.position;
            unit2.transform.position = tempPosition;
        }
    }


    //public void IncreaseUnitAttackPowerByPercentage(string unitName, float percentage)
    //{
    //    foreach (UnitEvolutionData unitData in unitEvolutionData)
    //    {
    //        if (unitData.unitName == unitName)
    //        {
    //            unitData.baseAttackPower += unitData.baseAttackPower * (percentage / 100f);
    //            break;
    //        }
    //    }
    //} //라운드보상

}