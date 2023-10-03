using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UnitEvolutionData
{
    public string unitName;
    public int maxLevel = 5;
    public GameObject[] unitPrefabs;
}

public class GameManager : MonoBehaviour
{
    public UnitEvolutionData[] unitEvolutionData; // Define this array in the inspector
    public Transform[] spawnPositions;
    private GameObject[] grid;
    private GameObject selectedUnit;
    private bool isFirstClick = true;
    public int movesPerRound = 5; // 예: 5회의 이동 횟수를 가진다고 가정
    private int currentMoves = 0;
    private MonsterSponer monsterSpawner;
    public Text movesText;
    [System.Serializable]
    public class RoundConfig
    {
        public int movesPerRound;
        public int monstersToSpawn;
        public float spawnInterval;
    }

    public RoundConfig[] rounds;
    private int currentRound = 0;


    void Start()
    {
        monsterSpawner = FindObjectOfType<MonsterSponer>();

        grid = new GameObject[spawnPositions.Length];
        InitGrid();

        UpdateMovesText();

        SetupRound();
    }

    void SetupRound()
    {
        Debug.Log("Setting up round: " + currentRound);
        if (currentRound < rounds.Length)
        {
            movesPerRound = rounds[currentRound].movesPerRound;
            monsterSpawner.monstersPerSpawn = rounds[currentRound].monstersToSpawn;
            monsterSpawner.spawnInterval = rounds[currentRound].spawnInterval;
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

        Debug.Log("OnMonsterSpawned called");
        if (--rounds[currentRound].monstersToSpawn <= 0)
        {
            monsterSpawner.spawnStarted = false; // 스폰 중지
            currentRound++; // 다음 라운드로
            SetupRound(); // 라운드 설정
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
                    currentMoves = 0; // 이동 횟수를 리셋s

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