using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
}