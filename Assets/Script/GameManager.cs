using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{public GameObject[] unitPrefabs; // 4 종류의 유닛 프리팹
    public Transform[] spawnPositions; // 36개의 스폰 위치

    private GameObject[] grid;
    private GameObject selectedUnit;

    void Start()
    {
        grid = new GameObject[spawnPositions.Length];
        InitGrid();
    }

    void InitGrid()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            GameObject unit = Instantiate(unitPrefabs[Random.Range(0, unitPrefabs.Length)], spawnPositions[i].position, Quaternion.identity);
            unit.GetComponent<Unit>().SetGridPosition(i);
            grid[i] = unit;
        }
    }

    public void OnUnitClicked(GameObject unit)
    {
       if (selectedUnit == null)
    {
        selectedUnit = unit;
        //셀렉된 유닛 하이라이트
        // selectedUnit.GetComponent<SpriteRenderer>().color = Color.blue;
    } else
    {
        if (selectedUnit == unit)
        {
            selectedUnit = null;
            unit.GetComponent<SpriteRenderer>().color = Color.white;
            return;
        }
            if (IsAdjacent(selectedUnit, unit))
            {
                SwapUnits(selectedUnit, unit);

                if (selectedUnit.GetComponent<Unit>().unitType == unit.GetComponent<Unit>().unitType)
                { // 두 유닛의 종류가 같으면 첫번째 유닛을 삭제하고 두번째 유닛을 업그레이드.
                int randomUnitType = Random.Range(0, unitPrefabs.Length);
                 GameObject newUnit1 = Instantiate(unitPrefabs[randomUnitType], selectedUnit.transform.position, Quaternion.identity);
                GameObject newUnit2 = Instantiate(unitPrefabs[randomUnitType], unit.transform.position, Quaternion.identity);
                Destroy(selectedUnit);
                Destroy(unit);
                selectedUnit = newUnit1;
                 unit = newUnit2;

                // 두번째 유닛을 업그레이드하는 코드 - 임시로 빨간색으로 바꿈 
                selectedUnit.GetComponent<SpriteRenderer>().color = Color.red;
                }

                selectedUnit = null;
            }
            else
            {
                
                selectedUnit = unit;
                // selectedUnit.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
    }

    bool IsAdjacent(GameObject unit1, GameObject unit2)
    {
        // 인접 유닛을 확인하는 새로운 방법을 구현해야 함
        // 예를 들어, 유닛 사이의 거리를 사용하여:
        return Vector3.Distance(unit1.transform.position, unit2.transform.position) < 1.5f; // 1.5는 인접한 유닛 사이의 최대 거리임
    }

    void SwapUnits(GameObject unit1, GameObject unit2)
    {
    Vector3 tempPosition = unit1.transform.position;
    unit1.transform.position = unit2.transform.position;
    unit2.transform.position = tempPosition;

}
}
