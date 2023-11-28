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
    public float AttackDamage = 17f;
    private float poisonDamagePercentage = 0.05f;
    private GameObject target;
    private Coroutine laserAttackCoroutine;
    private Unit unitScript;
    private bool isPrestige = false;

     private static bool globalSpecialAApplied = false;
    private static bool globalSpecialBApplied = false;

      bool isSpecialA=false;
     bool isSpecialB=false;

     private int MaxPoisonStacks=3;

    private void Start()
    {
        if (GameManager.Instance.unitEvolutionData[2].isPrestige==true)
        {
            Debug.Log("LilyPrestige");
            isPrestige = true;

        }
        unitScript = GetComponent<Unit>();
        // Start the laser attack loop
        laserAttackCoroutine = StartCoroutine(LaserAttackLoop());

         if (unitScript.unitLevel == 1)
                {
                    poisonDamagePercentage = 0.05f;
                      
                }
                else if (unitScript.unitLevel == 3)
                {
                    poisonDamagePercentage = 0.1f;
                    
                }
                else if (unitScript.unitLevel == 5)
                {
                    poisonDamagePercentage = 0.20f;
                    MaxPoisonStacks+=2;
                    
                }

                if(globalSpecialAApplied)
                {
                    poisonDamagePercentage+=0.05f;
                }

                if(globalSpecialBApplied)
                {
                    poisonDamagePercentage+=0.1f;
                    MaxPoisonStacks+=2;

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
                yield return new WaitForSeconds(1 / 1.7f); // 1.7 times per second
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
        if(isSpecialA)
        {
            SpecialA();
            
        }

        if(isSpecialB)
        {
            SpecialB();
        }
        Debug.Log("독스택"+poisonDamagePercentage);
        IDamageable damageableEntity = target.GetComponent<IDamageable>();
        if (damageableEntity != null)
        {
            
            
               

            damageableEntity.MaxPoisonStacks = MaxPoisonStacks;

            damageableEntity.TakeDamage(AttackDamage);
            damageableEntity.PoisonStack(poisonDamagePercentage * AttackDamage);
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
           
            damageableEntity.TakeDamage(AttackDamage);
            damageableEntity.EnhancedPoisonStack(poisonDamagePercentage,AttackDamage);
        }
    }
    GameObject FindClosestMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject monster in monsters)
        {
            float curDistance = Vector2.Distance(transform.position, monster.transform.position);
            if (curDistance < distance)
            {
                closest = monster;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void SpecialA()
    {   if(!globalSpecialAApplied)
        {
            poisonDamagePercentage+=0.05f;
            globalSpecialAApplied=true;
        }

    }

    public void SpecialB()
    {
        if(!globalSpecialBApplied)
        {
            poisonDamagePercentage+=0.1f;
            MaxPoisonStacks+=2;
            globalSpecialBApplied=true;

        }
    }
}

