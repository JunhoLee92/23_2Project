using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[System.Serializable]
public class UnitEvolutionData
{
    public string unitName;
    public int maxLevel = 5;
    public GameObject[] unitPrefabs;
    public bool isChecked; 
    public bool isPrestige;
    public Sprite thumbnailSprite;
    public Sprite standingSprite;

    public float damage;

    public float attackSpeed;
}