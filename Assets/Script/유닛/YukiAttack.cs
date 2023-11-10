using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukiAttack : MonoBehaviour
{
    public float attackDamage = 12f;
    public float attackSpeed = 1.4f;
    public GameObject projectilePrefab;  // Reference to the projectile sprite prefab
    /*public float attackRange = 50f; */      // Range within which Yuki starts attacking
    private float attackInterval;
    private float nextAttackTime = 0f;
    private Unit unitScript;

    void Start()
    {
        attackInterval = 1f / attackSpeed;

        unitScript = GetComponent<Unit>();

        // Add the following debug lines
       

    }

    void Update()
    {
        // Find the closest monster within attack range
        GameObject target = FindClosestMonster();

        if (target && Time.time >= nextAttackTime)
        {
            Shoot(target);
            nextAttackTime = Time.time + attackInterval;
        }

       
    }

    GameObject FindClosestMonster()
    {
        // This will find the closest monster based on distance within the attack range. 
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject monster in monsters)
        {
            float curDistance = Vector2.Distance(transform.position, monster.transform.position);
            if (curDistance < distance /*&& curDistance <= attackRange*/)
            {
                closest = monster;
                distance = curDistance;
            }
        }
        return closest;
    }

    void Shoot(GameObject target)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.target = target;
        projectileScript.damage = attackDamage;

        // Accessing the Unit script to get Yuki's current level
        Unit unitScript = GetComponent<Unit>();

        if (unitScript && unitScript.unitType == 1)  // Checking if it's Yuki
        {
            Debug.Log("Unit Type: " + unitScript.unitType);
            Debug.Log("Unit Level: " + unitScript.unitLevel);

            float freezeProbability = 0f;  // Initialize freeze probability

            if (unitScript.unitLevel == 1)
            {
                freezeProbability = 0.15f;

            }
            else if (unitScript.unitLevel == 3)
            {
                freezeProbability = 0.3f;
            }

           

            // Check if we should apply the freeze effect based on the probability
            if (Random.Range(0f,1f) <= freezeProbability)
            {
                
               
                MonsterController monsterScript = target.GetComponent<MonsterController>();
                if (monsterScript)
                {
                    float freezeDuration = 1f;  // Default freeze duration
                    if (unitScript.unitLevel == 5)
                    {
                        freezeDuration += 0.2f;
                    }
                    monsterScript.Slow(0.4f, freezeDuration);  // 1f slow amount means complete stop
                }
                Debug.Log("FrozenCheck");
            }
        }
    }
}
