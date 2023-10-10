using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int unitType; // Type of unit (0, 1, 2, 3)
    public int gridIndex; // Grid index of unit
    public int unitLevel = 0; // Unit's level (initialize to level 0)
    public float attackPower = 10f; // Unit의 공격력


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

    public void IncreaseAttackPowerByPercentage(float percentage)
    {
        attackPower += attackPower * (percentage / 100f);
    }
}