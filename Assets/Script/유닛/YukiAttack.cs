using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukiAttack : MonoBehaviour
{
    public float attackDamage = 12f;
    public float attackSpeed = 1.4f;
    public GameObject projectilePrefab;  // Reference to the projectile sprite prefab
    private float attackRange = 8f;    // Range within which Yuki starts attacking
    private float attackInterval;
    private float nextAttackTime = 0f;
    private Unit unitScript;
    float blizzardCooltime = 10.0f;
    float nextBlizzardTime = 0.0f;

    float freezeDuration = 1f;
    static float freezeProbability = 0.15f; 

    public static bool isSpecialA=false;
    public static bool isSpecialB=false;

    bool isBoolA=false;
    bool isBoolB=false;
    

    void Start()
    {
        attackInterval = 1f / attackSpeed;

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

        nextBlizzardTime = Time.time + blizzardCooltime;

        //  if (unitScript.unitLevel == 1)
        //     {
        //         freezeProbability = 0.15f;

        //     }
        //     else if (unitScript.unitLevel >= 3)
        //     {
        //         freezeProbability = 0.3f;
        //     }

        //       else if (unitScript.unitLevel == 5)
        //             {
        //                 freezeDuration += 0.2f;
        //             }



          
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


        // Add the following debug lines


    }

    void Update()
    {
        // Find the closest monster within attack range
        GameObject target = FindClosestMonster();
        attackInterval = 1f / attackSpeed;

        if (target && Time.time >= nextAttackTime)
        {
            Shoot(target);
            nextAttackTime = Time.time + attackInterval;
        }

        if (Time.time >= nextBlizzardTime && GameManager.Instance.unitEvolutionData[1].isPrestige == true)
        {
            blizzard();
            nextBlizzardTime = Time.time + blizzardCooltime;
        }

        if (Input.GetKeyDown(KeyCode.Q)) //for special test
        {
            isSpecialA = true;
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
        float distance = attackRange;

        foreach (GameObject monster in monsters)
        {
            float curDistance = Vector2.Distance(transform.position, monster.transform.position);
            if (curDistance < distance && curDistance <= attackRange)
            {
                closest = monster;
                distance = curDistance;
            }
        }
        return closest;
    }

    void Shoot(GameObject target)
    {    Unit unitScript = GetComponent<Unit>();

        if(!isBoolA&&isSpecialA&&unitScript.unitLevel>=3)
        {
            SpecialA();
            isSpecialA=false;
        }
        if(!isBoolB&&isSpecialB&&unitScript.unitLevel==5)
        {
            SpecialB();
            isSpecialB=false;
        }
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.target = target;
        projectileScript.damage = attackDamage;

        // Accessing the Unit script to get Yuki's current level
       
        
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

  void OnDestroy()
    {
        if (unitScript != null)
        {
            unitScript.OnAttackDamageChanged -= UpdateDamage;
            unitScript.OnAttackSpeedChanged -= UpdateSpeed;
            
        }
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
       
        freezeProbability+=0.15f;
       
    }

    public void SpecialB()
    {
          freezeDuration+=0.2f;
         
    }
}

