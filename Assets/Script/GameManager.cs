using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class GameManager : MonoBehaviour
//{   public GameObject[] unitPrefabs; // 4 종류의 유닛 프리팹
//    public Transform[] spawnPositions; // 36개의 스폰 위치

//    private GameObject[] grid;
//    private GameObject selectedUnit;


//    [System.Serializable]
//    public class UnitEvolutionData
//    {
//        public string unitName; // Name of the unit (e.g., "A Unit")
//        public int maxLevel; // Maximum level for this unit (e.g., 5)
//        public GameObject[] unitPrefabs; // Prefabs for each level (index 0 is level 0, index 1 is level 1, etc.)
//    }


//    void Start()
//    {
//        grid = new GameObject[spawnPositions.Length];
//        InitGrid();
//    }

//    void InitGrid()
//    {
//        for (int i = 0; i < spawnPositions.Length; i++)
//        {
//            GameObject unit = Instantiate(unitPrefabs[Random.Range(0, unitPrefabs.Length)], spawnPositions[i].position, Quaternion.identity);
//            unit.GetComponent<Unit>().SetGridPosition(i);
//            grid[i] = unit;
//        }
//    }

//    public void OnUnitClicked(GameObject unit)
//    {
//       if (selectedUnit == null)
//    {
//        selectedUnit = unit;
//        //셀렉된 유닛 하이라이트
//        // selectedUnit.GetComponent<SpriteRenderer>().color = Color.blue;
//    } else
//    {
//        if (selectedUnit == unit)
//        {
//            selectedUnit = null;
//            unit.GetComponent<SpriteRenderer>().color = Color.white;
//            return;
//        }
//            if (IsAdjacent(selectedUnit, unit))
//            {
//                SwapUnits(selectedUnit, unit);

//                if (selectedUnit.GetComponent<Unit>().unitType == unit.GetComponent<Unit>().unitType)
//                { // 두 유닛의 종류가 같으면 첫번째 유닛을 삭제하고 두번째 유닛을 업그레이드.
//                int randomUnitType = Random.Range(0, unitPrefabs.Length);
//                 GameObject newUnit1 = Instantiate(unitPrefabs[randomUnitType], selectedUnit.transform.position, Quaternion.identity);
//                GameObject newUnit2 = Instantiate(unitPrefabs[randomUnitType], unit.transform.position, Quaternion.identity);
//                Destroy(selectedUnit);
//                Destroy(unit);
//                selectedUnit = newUnit1;
//                 unit = newUnit2;

//                // 두번째 유닛을 업그레이드하는 코드 - 임시로 빨간색으로 바꿈 
//                selectedUnit.GetComponent<SpriteRenderer>().color = Color.red;
//                }

//                selectedUnit = null;
//            }
//            else
//            {

//                selectedUnit = unit;
//                // selectedUnit.GetComponent<SpriteRenderer>().color = Color.blue;
//            }
//        }
//    }

//    bool IsAdjacent(GameObject unit1, GameObject unit2)
//    {
//        // 인접 유닛을 확인하는 새로운 방법을 구현해야 함
//        // 예를 들어, 유닛 사이의 거리를 사용하여:
//        return Vector3.Distance(unit1.transform.position, unit2.transform.position) < 1.5f; // 1.5는 인접한 유닛 사이의 최대 거리임
//    }

//    void SwapUnits(GameObject unit1, GameObject unit2)
//    {
//    Vector3 tempPosition = unit1.transform.position;
//    unit1.transform.position = unit2.transform.position;
//    unit2.transform.position = tempPosition;

//}
//}
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
   


    void Start()
    {
        grid = new GameObject[spawnPositions.Length];
        InitGrid();
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

    //public void OnUnitClicked(GameObject unit)
    //{
    //    if (isFirstClick)
    //    {
    //        selectedUnit = unit;
    //        isFirstClick = false;
    //        // Highlight selected units
    //        // selectedUnit.GetComponent<SpriteRenderer>().color = Color.blue;
    //    }
    //    else
    //    {
    //        if (selectedUnit == unit)
    //        {
    //            selectedUnit = null;
    //            isFirstClick = true;
    //            unit.GetComponent<SpriteRenderer>().color = Color.white;

    //            // Create a new level 0 unit of the same type in the position of the first clicked unit
    //            int newUnitType = selectedUnit.GetComponent<Unit>().unitType;
    //            int newUnitLevel = 0; // Always create a level 0 unit
    //            GameObject newUnitPrefab = unitEvolutionData[newUnitType].unitPrefabs[newUnitLevel];
    //            GameObject newUnit = Instantiate(newUnitPrefab, selectedUnit.transform.position, Quaternion.identity);
    //            newUnit.GetComponent<Unit>().SetGridPosition(selectedUnit.GetComponent<Unit>().gridIndex);
    //            return;
    //        }

    //        // Calculate the distance between the two units
    //        float distance = Vector3.Distance(selectedUnit.transform.position, unit.transform.position);

    //        if (distance < 1.5f) // Adjust this threshold as needed
    //        {
    //            if (selectedUnit.GetComponent<Unit>().unitType == unit.GetComponent<Unit>().unitType)
    //            {
    //                // Get the current level of the second unit
    //                int currentLevel = unit.GetComponent<Unit>().unitLevel;

    //                // Ensure the new level doesn't exceed the maximum level
    //                int newLevel = currentLevel + 1;
    //                if (newLevel <= unitEvolutionData[unit.GetComponent<Unit>().unitType].maxLevel)
    //                {
    //                    // Instantiate the new unit prefab for the second unit
    //                    GameObject newUnitPrefab = unitEvolutionData[unit.GetComponent<Unit>().unitType].unitPrefabs[newLevel];
    //                    GameObject newUnit = Instantiate(newUnitPrefab, unit.transform.position, Quaternion.identity);
    //                    newUnit.GetComponent<Unit>().SetGridPosition(unit.GetComponent<Unit>().gridIndex);

    //                    // Destroy the second unit
    //                    Destroy(unit);
    //                }

    //                // Create a random level 0 unit at the location of the first unit
    //                int randomUnitType = Random.Range(0, unitEvolutionData.Length); // Choose a random unit type
    //                GameObject randomUnitPrefab = unitEvolutionData[randomUnitType].unitPrefabs[0]; // Level 0 prefab
    //                GameObject newRandomUnit = Instantiate(randomUnitPrefab, selectedUnit.transform.position, Quaternion.identity);
    //                newRandomUnit.GetComponent<Unit>().SetGridPosition(selectedUnit.GetComponent<Unit>().gridIndex);

    //                // Destroy the first unit
    //                Destroy(selectedUnit);
    //            }
    //            else
    //            {
    //                // Swap positions of the two units
    //                SwapUnits(selectedUnit, unit);
    //            }
    //        }

    //        selectedUnit = null;
    //        isFirstClick = true;
    //    }
    //}
    //public void OnUnitClicked(GameObject unit)
    //{
    //    if (isFirstClick)
    //    {
    //        selectedUnit = unit;
    //        isFirstClick = false;
    //        // Highlight selected units
    //        // selectedUnit.GetComponent<SpriteRenderer>().color = Color.blue;
    //    }
    //    else
    //    {
    //        if (selectedUnit == unit)
    //        {
    //            selectedUnit = null;
    //            isFirstClick = true;
    //            unit.GetComponent<SpriteRenderer>().color = Color.white;

    //            // Create a new level 0 unit of the same type in the position of the first clicked unit
    //            int newUnitType = selectedUnit.GetComponent<Unit>().unitType;
    //            int newUnitLevel = 0; // Always create a level 0 unit
    //            GameObject newUnitPrefab = unitEvolutionData[newUnitType].unitPrefabs[newUnitLevel];
    //            GameObject newUnit = Instantiate(newUnitPrefab, selectedUnit.transform.position, Quaternion.identity);
    //            newUnit.GetComponent<Unit>().unitType = newUnitType; // Set the correct unit type
    //            newUnit.GetComponent<Unit>().SetGridPosition(selectedUnit.GetComponent<Unit>().gridIndex);
    //            return;
    //        }

    //        // Calculate the distance between the two units
    //        float distance = Vector3.Distance(selectedUnit.transform.position, unit.transform.position);

    //        if (distance < 1.5f) // Adjust this threshold as needed
    //        {
    //            if (selectedUnit.GetComponent<Unit>().unitType == unit.GetComponent<Unit>().unitType)
    //            {
    //                // Get the current level of the second unit
    //                int currentLevel = unit.GetComponent<Unit>().unitLevel;

    //                // Ensure the new level doesn't exceed the maximum level
    //                int newLevel = currentLevel + 1;
    //                if (newLevel <= unitEvolutionData[unit.GetComponent<Unit>().unitType].maxLevel)
    //                {
    //                    // Instantiate the new unit prefab for the second unit
    //                    GameObject newUnitPrefab = unitEvolutionData[unit.GetComponent<Unit>().unitType].unitPrefabs[newLevel];
    //                    GameObject newUnit = Instantiate(newUnitPrefab, unit.transform.position, Quaternion.identity);
    //                    newUnit.GetComponent<Unit>().unitType = unit.GetComponent<Unit>().unitType; // Set the correct unit type
    //                    newUnit.GetComponent<Unit>().SetGridPosition(unit.GetComponent<Unit>().gridIndex);
    //                    newUnit.GetComponent<Unit>().unitLevel = newLevel; // Set the new level

    //                    // Destroy the second unit
    //                    Destroy(unit);
    //                }

    //                // Create a random level 0 unit at the location of the first unit
    //                int randomUnitType = Random.Range(0, unitEvolutionData.Length); // Choose a random unit type
    //                GameObject randomUnitPrefab = unitEvolutionData[randomUnitType].unitPrefabs[0]; // Level 0 prefab
    //                GameObject newRandomUnit = Instantiate(randomUnitPrefab, selectedUnit.transform.position, Quaternion.identity);
    //                newRandomUnit.GetComponent<Unit>().unitType = randomUnitType; // Set the correct unit type
    //                newRandomUnit.GetComponent<Unit>().SetGridPosition(selectedUnit.GetComponent<Unit>().gridIndex);

    //                // Destroy the first unit
    //                Destroy(selectedUnit);
    //            }
    //            else
    //            {
    //                // Swap positions of the two units
    //                SwapUnits(selectedUnit, unit);
    //            }
    //        }

    //        selectedUnit = null;
    //        isFirstClick = true;
    //    }
    //}
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
            }

            selectedUnit = null;
            isFirstClick = true;
        }
    }
    void SwapUnits(GameObject unit1, GameObject unit2)
    {
        Vector3 tempPosition = unit1.transform.position;
        unit1.transform.position = unit2.transform.position;
        unit2.transform.position = tempPosition;
    }
}