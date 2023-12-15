using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Aire : MonoBehaviour
{
    public float attackDamage = 7f;
   
    
   
    private Unit unitScript;
    


    public static bool isSpecialA = false;
    public static bool isSpecialB = false;

    bool isBoolA = false;
    bool isBoolB = false;


    void Start()
    {

        unitScript = GetComponent<Unit>();

        if (unitScript != null)
        {
            unitScript.OnAttackDamageChanged += UpdateDamage;
            
            attackDamage = unitScript.AttackPower; // Initialize with current attack damage
           
            Debug.Log("Subscribed to OnAttackDamageChanged");
        }

        attackDamage = unitScript.attackPower;





        if (isSpecialA && unitScript.unitLevel >= 3)
        {
            SpecialA();
            isBoolA = true;
        }

        if (isSpecialA && unitScript.unitLevel == 5)
        {
            SpecialB();
            isBoolB = true;
        }


    }

    void Update()
    {



        if (Input.GetKeyDown(KeyCode.Q)) //for special test
        {
            isSpecialA = true;
            // SpecialA();
        }

    }




    private void UpdateDamage(float newDamage)
    {
        attackDamage = newDamage;
        // Additional logic to handle damage change
        Debug.Log("UPdateDamageAire" + attackDamage);
    }

   

    void OnDestroy()
    {
        if (unitScript != null)
        {
            unitScript.OnAttackDamageChanged -= UpdateDamage;
            

        }
    }

  

    public void SpecialA()
    {


    }

    public void SpecialB()
    {

    }

}
