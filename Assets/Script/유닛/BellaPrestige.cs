using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellaPrestige : MonoBehaviour
{
    BellaAttack currentStrongestBella = null;

    // Start is called before the first frame update
    void Start()
    {
        FindAllBellas();
    }

    // Update is called once per frame
    void Update()
    {
        
      
    }

    public BellaAttack[] FindAllBellas()
    {
        return FindObjectsOfType<BellaAttack>();
    }

   public void UpdateStrongestBella()
    {
        BellaAttack[] allBellas = FindObjectsOfType<BellaAttack>();
        BellaAttack strongestBella = null;
        int highestLevel = -1;

        foreach (BellaAttack bella in allBellas)
        {
            Unit bellaUnit = bella.GetComponent<Unit>();
            if (bellaUnit == null) continue;

            if (bellaUnit.unitLevel > highestLevel)
            {
                highestLevel = bellaUnit.unitLevel;
                strongestBella = bella;
            }
        }

        if (strongestBella != currentStrongestBella)
        {
            if (currentStrongestBella != null)
            {
                // Reset the previous strongest Bella's attack damage to base value
                currentStrongestBella.ResetAttackDamageToBase();
            }

            if (strongestBella != null)
            {
                // Increase the new strongest Bella's attack damage
                strongestBella.IncreaseAttackDamage(1.1f);
            }

            currentStrongestBella = strongestBella;
        }
    }
}

