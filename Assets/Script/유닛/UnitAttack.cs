using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{

    public float attackDamage = 21f;       // Attack power
    public float attackRange=1;         // Attack range (1 block)
    public float attackSpeed = 2.2f;       // Attacks per second
    public GameObject attackEffectPrefab;  // Reference to the attack effect sprite prefab
    public float effectDuration = 0.5f;    // Duration for which the effect sprite is visible

    private float attackInterval;          // Time between attacks
    private float nextAttackTime = 0f;     // Time when next attack can occur
    private Unit unitScript;
    float scalex=1;
    float scaley=1;

   public static bool isSpecialA=false;

   public static bool isSpecialB=false;
    

    bool isBoolA=false;
     bool isBoolB=false;

    void Start()
    {
        if (GameManager.Instance.unitEvolutionData[0].isPrestige==true)
        {
            Debug.Log("KaliPrestige");
            attackSpeed = attackSpeed * 1.5f;
            Debug.Log("KaliAttackSpeed" + attackSpeed);
        }
        unitScript = GetComponent<Unit>();
        attackInterval = 1f / attackSpeed;  // Calculate the time between attacks

    
        // if (unitScript.unitLevel == 1 || unitScript.unitLevel==2)
        // {
        //     attackRange *= 1f;
        //     scalex *= 1f;
        //     scaley *= 1f;

        // }
        // else if (unitScript.unitLevel == 3 || unitScript.unitLevel==4)
        // {
        //     attackRange = attackRange*1.2f;
        //     scalex = scalex*1.2f;
        //     scaley = scaley*1.2f;

        // }

        // else if (unitScript.unitLevel == 5)
        // {
        //     attackRange = attackRange*1.5f;
        //     scalex = scalex*1.5f;
        //     scaley = scaley*1.5f;
        // }
        if(isSpecialA && unitScript.unitLevel>=3)
        {
            SpecialA();
            isBoolA=true;
        }

        if(isSpecialA && unitScript.unitLevel==5)
        {
            SpecialB();
            isBoolB=true;
        }
        Debug.Log("범위"+attackRange);
    }

    void Update()
    {
        // Check if it's time to attack
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackInterval;
        }

        if(Input.GetKeyDown(KeyCode.Q)) //for special reward test
        {
            isSpecialB=true;
            Debug.Log("special");

        }
    }

    void Attack()
    {
      if(!isBoolA&&isSpecialA&&unitScript.unitLevel>=3)
      {
        Debug.Log("SpecialA");
        SpecialA();
        isBoolA=true;
      }

       if(!isBoolB&&isSpecialB&&unitScript.unitLevel==5)
      {
        Debug.Log("SpecialB");
        SpecialB();
        isBoolA=true;
      }

        // Detect monsters within attack range using a simple overlap check
        Collider2D[] hitEntities = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (var entity in hitEntities)
        {
            IDamageable damageableEntity = entity.GetComponent<IDamageable>();
            if (damageableEntity != null)
            {
                // Apply damage to the entity (monster or boss)
                damageableEntity.TakeDamage(attackDamage);

                // Instantiate the attack effect sprite
                GameObject effectInstance = Instantiate(attackEffectPrefab, this.transform.position, Quaternion.identity);
                effectInstance.transform.localScale = new Vector3(scalex, scaley, 1.0f);
                // Destroy the effect sprite after the set duration
                Destroy(effectInstance, effectDuration);
            }
        }
    }

   public void SpecialA()
    {
        
        
            attackRange *= 1.2f;
            scalex *= 1.2f;
            scaley *= 1.2f;
           
        
        
     }

   public void SpecialB()
    {   
     
            attackRange *= 1.3f;
            scalex *= 1.3f;
            scaley *= 1.3f;
         
    }
}

