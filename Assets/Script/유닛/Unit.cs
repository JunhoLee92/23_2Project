using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int unitType; // 유닛의 종류 (0, 1, 2, 3)
    public int gridIndex; // 유닛의 그리드 인덱스

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnMouseDown()
    {
        gameManager.OnUnitClicked(gameObject);
    }

    public void SetGridPosition(int index)
    {
        gridIndex = index;
    }
}
