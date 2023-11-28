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
    float blizzardCooltime = 10.0f;
    float nextBlizzardTime = 0.0f;

    float freezeDuration = 1f;
    static float freezeProbability = 0.15f; 

    bool isSpecialA=false;
    bool isSpecialB=false;
    static bool globalSpecialAApplied=false;

    static bool globalSpecialBApplied=false;

    void Start()
    {
        attackInterval = 1f / attackSpeed;

        unitScript = GetComponent<Unit>();

        nextBlizzardTime = Time.time + blizzardCooltime;

         if (unitScript.unitLevel == 1)
            {
                freezeProbability = 0.15f;

            }
            else if (unitScript.unitLevel >= 3)
            {
                freezeProbability = 0.3f;
            }

              else if (unitScript.unitLevel == 5)
                    {
                        freezeDuration += 0.2f;
                    }



            if(globalSpecialAApplied)
            {
                freezeProbability+=0.15f;
            }

            if(globalSpecialBApplied)
            {
                freezeDuration+=0.2f;
            }


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

        if(Time.time>=nextBlizzardTime && GameManager.Instance.unitEvolutionData[1].isPrestige == true)
        {
            blizzard();
            nextBlizzardTime = Time.time + blizzardCooltime;
        }

        if(Input.GetKeyDown(KeyCode.Q)) //for special test
        {
            isSpecialA=true;
            // SpecialA();
        }

    }
    GameObject[] FindAllMonsters()
    {
        return GameObject.FindGameObjectsWithTag("Monster");
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
        if(isSpecialA)
        {
            SpecialA();
            isSpecialA=false;
        }
        if(isSpecialB)
        {
            SpecialB();
            isSpecialB=false;
        }
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.target = target;
        projectileScript.damage = attackDamage;

        // Accessing the Unit script to get Yuki's current level
        Unit unitScript = GetComponent<Unit>();

        
             // Initialize freeze probability

           


            // Check if we should apply the freeze effect based on the probability
            if (Random.Range(0f, 1f) <= freezeProbability)
            {


                MonsterController monsterScript = target.GetComponent<MonsterController>();
                if (monsterScript)
                {
                      // Default freeze duration
                  
                    monsterScript.Slow(0.4f, freezeDuration);  // 1f slow amount means complete stop
                }
                Debug.Log("FrozenCheck");
            }
        

        Debug.Log("둔화확룔"+freezeProbability);
    }

    void blizzard()
    {
        GameObject[] allMonsters =FindAllMonsters();
        
        foreach (GameObject monster in allMonsters)
        {
            MonsterController monsterScript = monster.GetComponent<MonsterController>();
            monsterScript.Slow(1.0f, 1.0f);
        }

        Debug.Log("Blizzard");
    }    

    public void SpecialA()
    {
        isSpecialA=true;
        if(!globalSpecialAApplied)
        {
        freezeProbability+=0.15f;
        globalSpecialAApplied=true;
        Debug.Log("스페셜1적용");
        }
    }

    public void SpecialB()
    {
        isSpecialB=true;
        if(!globalSpecialBApplied)
        {
            freezeDuration+=0.2f;
            globalSpecialBApplied=true;
        }
    }
}

