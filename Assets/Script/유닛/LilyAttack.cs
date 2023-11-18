using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LilyAttack : MonoBehaviour
{

  


    public GameObject laserPrefab;
    public float AttackDamage = 17f;
    private float poisonDamagePercentage = 0.15f;
    private GameObject target;
    private Coroutine laserAttackCoroutine;
    private Unit unitScript;
    private bool isPrestige = false;

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

        IDamageable damageableEntity = target.GetComponent<IDamageable>();
        if (damageableEntity != null)
        {
            if (unitScript && unitScript.unitType == 2)  // Checking if it's Lily
            {
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
                    damageableEntity.MaxPoisonStacks = 5;
                }
            }

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
            if (unitScript && unitScript.unitType == 2)  // Checking if it's Lily
            {
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
                    damageableEntity.MaxPoisonStacks = 5;
                }
            }

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
}

