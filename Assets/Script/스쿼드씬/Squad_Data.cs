using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Squad_Data : MonoBehaviour
{
    public static Squad_Data Instance { get; private set; }

    public UnitEvolutionData[] unitEvolutionData;

    public List<string> selectedUnitNames = new List<string>(); 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



  

    //public void ToggleChecked(string unitName)
    //{
    //    bool wasChecked = false;
    //    foreach (var unit in unitEvolutionData)
    //    {
    //        if (unit.unitName == unitName)
    //        {
    //            wasChecked = unit.isChecked;
    //            unit.isChecked = !unit.isChecked;
    //            break;
    //        }
    //    }

    //    if (!wasChecked)
    //    {
    //        selectedUnitNames.Add(unitName);
    //    }
    //    else
    //    {
    //        selectedUnitNames.Remove(unitName);
    //    }

    //    UpdatePrestigeStatus();
    //}

    public void ToggleChecked(string unitName)
    {
        bool wasChecked = false;
        foreach (var unit in unitEvolutionData)
        {
            if (unit.unitName == unitName)
            {
                wasChecked = unit.isChecked;
                unit.isChecked = !unit.isChecked;
                break;
            }
        }

        if (!wasChecked)
        {

            int emptyIndex = selectedUnitNames.IndexOf("");
            if (emptyIndex != -1)
            {
                selectedUnitNames[emptyIndex] = unitName;
            }
            else
            {
                selectedUnitNames.Add(unitName);
            }
        }
        else
        {
            // add empty string 
            int index = selectedUnitNames.IndexOf(unitName);
            if (index != -1)
            {
                selectedUnitNames[index] = "";
            }
        }

        UpdatePrestigeStatus();
    }

    void UpdatePrestigeStatus()
    {
        // reset all unit's isprestige
        foreach (var unit in unitEvolutionData)
        {
            unit.isPrestige = false;
        }

        // firstclicked unit = prestige unit
        if (selectedUnitNames.Count > 0)
        {
            string leaderName = selectedUnitNames[0];
            var leaderUnit = Array.Find(unitEvolutionData, u => u.unitName == leaderName);
            if (leaderUnit != null)
            {
                leaderUnit.isPrestige = true;
            }
        }

        //Squad_UIManager.Instance.UpdateStandingImage();
    }

    public void ResetSquadSelection()
    {
        foreach (var unit in unitEvolutionData)
        {
            unit.isChecked = false;
            unit.isPrestige = false;
        }
        selectedUnitNames.Clear();
        //Squad_UIManager.Instance.standingImage.sprite = null;


        //Squad_UIManager.Instance.UpdateUnitThumbnails();
    }
}
