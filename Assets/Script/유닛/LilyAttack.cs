using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LilyAttack : MonoBehaviour
{

  


    public GameObject laserPrefab;
    public float attackDamage = 17f;
    private float poisonDamagePercentage = 0.05f;
    private GameObject target;
    private Coroutine laserAttackCoroutine;
    private Unit unitScript;
    private float attackRange = 7f;
    public float attackSpeed;
    private bool isPrestige = false;

     public static bool isSpecialA=false;

   public static bool isSpecialB=false;

    bool isBoolA=false;
    bool isBoolB=false;

     private int MaxPoisonStacks=3;

    private void Start()
    {
        if (GameManager.Instance.unitEvolutionData[2].isPrestige==true)
        {
            Debug.Log("LilyPrestige");
            isPrestige = true;

        }
        unitScript = GetComponent<Unit>();

          if (unitScript != null)
        {
            unitScript.OnAttackDamageChanged += UpdateDamage;
            unitScript.OnAttackSpeedChanged += UpdateSpeed;
            attackDamage = unitScript.AttackPower; // Initialize with current attack damage
            attackSpeed =unitScript.AttackSpeed;
            Debug.Log("Subscribed to OnAttackDamageChanged");
        }
    

        attackDamage=unitScript.attackPower;
        // Start the laser attack loop
        laserAttackCoroutine = StartCoroutine(LaserAttackLoop());

        //  if (unitScript.unitLevel == 1)
        //         {
        //             poisonDamagePercentage = 0.05f;
                      
        //         }
        //         else if (unitScript.unitLevel == 3)
        //         {
        //             poisonDamagePercentage = 0.1f;
                    
        //         }
        //         else if (unitScript.unitLevel == 5)
        //         {
        //             poisonDamagePercentage = 0.20f;
        //             MaxPoisonStacks+=2;
                    
        //         }

        
        
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
                
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) //for special reward test
        {
            isSpecialA=true;
            Debug.Log("special");

        }
    }
    private void OnDestroy()
    {
        if (laserAttackCoroutine != null)
        {
            StopCoroutine(laserAttackCoroutine);
        }
           if (unitScript != null)
        {
            unitScript.OnAttackDamageChanged -= UpdateDamage;
            unitScript.OnAttackSpeedChanged -= UpdateSpeed;
            
        }
    }

    IEnumerator LaserAttackLoop()
    {
        while (true)
        {
            // Find the closest monster and shoot the laser
            target = FindClosestMonster();
            if (target != null)
            {
                ShootLaser(target);
                yield return new WaitForSeconds(1 / attackSpeed); // 1.7 times per second
            }
            else
            {
                yield return null; // Wait for the next frame if no target is found
            }
        }
    }

    

    void ShootLaser(GameObject target)
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity, transform);
        laser.transform.up = target.transform.position - transform.position;
        float laserLength = Vector2.Distance(transform.position, target.transform.position);
        laser.transform.localScale = new Vector2(0.5f, laserLength / 4.5f);


        if (isPrestige == false)
        {
            StartCoroutine(DealDamageWithDelay(target, 0.3f));
        }
        else if(isPrestige==true)
        {
            StartCoroutine(DealDamageWithDelayPrestige(target, 0.3f));
        }
        Destroy(laser,0.3f);
    }

    IEnumerator DealDamageWithDelay(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (target == null)  // Check if the target GameObject is destroyed
        {
            yield break;  // Exit the coroutine if the target is destroyed
        }
      
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

        Debug.Log("독스택"+poisonDamagePercentage);
        IDamageable damageableEntity = target.GetComponent<IDamageable>();
        if (damageableEntity != null)
        {
            
            
               

            damageableEntity.MaxPoisonStacks = MaxPoisonStacks;

            damageableEntity.TakeDamage(attackDamage);
            damageableEntity.PoisonStack(poisonDamagePercentage * attackDamage);
        }
    }

    IEnumerator DealDamageWithDelayPrestige(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (target == null)  // Check if the target GameObject is destroyed
        {
            yield break;  // Exit the coroutine if the target is destroyed
        }
            
        IDamageable damageableEntity = target.GetComponent<IDamageable>();
        if (damageableEntity != null)
        {
           
            damageableEntity.TakeDamage(attackDamage);
            damageableEntity.EnhancedPoisonStack(poisonDamagePercentage,attackDamage);
        }
    }
    GameObject FindClosestMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject closest = null;
        float distance = attackRange;

        foreach (GameObject monster in monsters)
        {
            float curDistance = Vector2.Distance(transform.position, monster.transform.position);
            if (curDistance < distance && curDistance<=attackRange)
            {
                closest = monster;
                distance = curDistance;
            }
        }
        return closest;
    }

    private void UpdateDamage(float newDamage)
    {
        attackDamage = newDamage;
        // Additional logic to handle damage change
        Debug.Log("UPdateDamageKali"+attackDamage);
    }

    private void UpdateSpeed(float newSpeed)
    {
        Debug.Log($"Before increase: AttackSpeed = {attackSpeed}, AttackDamage = {attackDamage}");
    // Logic to increase AttackSpeed
        attackSpeed=newSpeed;
         Debug.Log("UPdateSpeedKali"+attackSpeed);

           Debug.Log($"After increase: AttackSpeed = {attackSpeed}, AttackDamage = {attackDamage}");

    }

  

    public void SpecialA()
    {  
            poisonDamagePercentage+=0.05f;
         
    }

    public void SpecialB()
    {
        
            poisonDamagePercentage+=0.1f;
            MaxPoisonStacks+=2;
        
    }
}

