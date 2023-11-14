using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Squad_UIManager : MonoBehaviour
{
    public Image[] unitThumbnails;
    public Image standingImage;
    public Image StandingIdle;
    public static Squad_UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
         
        }
        else if (Instance == this)
        {
            {
                Destroy(gameObject);
            }
        }

    }

    void Update()
    {
        UpdateUnitThumbnails();
        UpdateStandingImage();
    }

    //void UpdateUnitThumbnails()
    //{
    //    for (int i = 0; i < unitThumbnails.Length; i++)
    //    {
    //        if (i < Squad_Data.Instance.selectedUnitNames.Count)
    //        {
    //            string unitName = Squad_Data.Instance.selectedUnitNames[i];
    //            UnitEvolutionData unitData = Array.Find(Squad_Data.Instance.unitEvolutionData, u => u.unitName == unitName);
    //            if (unitData != null)
    //            {
    //                unitThumbnails[i].sprite = unitData.thumbnailSprite;
    //            }
    //        }
    //        else
    //        {
    //            unitThumbnails[i].sprite = null; // 
    //        }
    //    }

    //    if (Squad_Data.Instance.selectedUnitNames.Count > 0)
    //    {
    //        string leaderName = Squad_Data.Instance.selectedUnitNames[0];
    //        var leaderUnit = Array.Find(Squad_Data.Instance.unitEvolutionData, u => u.unitName == leaderName);
    //        if (leaderUnit != null)
    //        {
    //            standingImage.sprite = leaderUnit.standingSprite;
    //        }
    //    }
    //    else
    //    {
    //        standingImage.sprite = null;
    //    }
    //}

    public void UpdateUnitThumbnails()
    {
        if (standingImage == null)
        {
            Debug.LogError("StandingImage component is not assigned in Squad_UIManager");
            return;
        }

        for (int i = 0; i < unitThumbnails.Length; i++)
        {
            if (i < Squad_Data.Instance.selectedUnitNames.Count)
            {
                string unitName = Squad_Data.Instance.selectedUnitNames[i];
                if (!string.IsNullOrEmpty(unitName))
                {
                    var unitData = Array.Find(Squad_Data.Instance.unitEvolutionData, u => u.unitName == unitName);
                    if (unitData != null)
                    {
                        unitThumbnails[i].sprite = unitData.thumbnailSprite;
                    }
                }
                else
                {
                    unitThumbnails[i].sprite = null; //
                }
            }
            else
            {
                unitThumbnails[i].sprite = null; // 
            }

        }

        //if (Squad_Data.Instance.selectedUnitNames.Count > 0 && !string.IsNullOrEmpty(Squad_Data.Instance.selectedUnitNames[0]))
        //{
        //    string leaderName = Squad_Data.Instance.selectedUnitNames[0];
        //    var leaderUnit = Array.Find(Squad_Data.Instance.unitEvolutionData, u => u.unitName == leaderName);
        //    if (leaderUnit != null)
        //    {
        //        standingImage.sprite = leaderUnit.standingSprite; // 
        //    }
        //    else
        //    {
        //        standingImage.sprite = null; // Delete Image If there's No Squadleader
        //    }
        //}
    }

    public void UpdateStandingImage()
    {
        if (Squad_Data.Instance.selectedUnitNames.Count > 0 && !string.IsNullOrEmpty(Squad_Data.Instance.selectedUnitNames[0]))
        {
            string leaderName = Squad_Data.Instance.selectedUnitNames[0];
            var leaderUnit = Array.Find(Squad_Data.Instance.unitEvolutionData, u => u.unitName == leaderName);
            if (leaderUnit != null)
            {
                standingImage.sprite = leaderUnit.standingSprite; // 
            }
            else
            {
                Debug.Log(leaderName);
                standingImage.sprite = StandingIdle.sprite; // Delete Image If there's No Squadleader
            }
        }
    }

   



}
