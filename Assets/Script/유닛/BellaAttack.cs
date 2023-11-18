using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellaAttack : MonoBehaviour
{
    public float attackDamage = 7f;
    public float attackSpeed = 1.0f;
    public GameObject projectilePrefab;
    private Unit unitScript;  

    private float attackInterval;
    private float nextAttackTime = 0f;
    private BellaPrestige bellapretige;

    private void Awake()
    {
        bellapretige = FindObjectOfType<BellaPrestige>();
        if (bellapretige == null)
        {
            Debug.LogError("BellaPrestige is Null");
        }
    }
    void Start()
    {
        if (GameManager.Instance.unitEvolutionData[5].isPrestige == true)
        {
            Debug.Log("BellaPrestige");
            bellapretige.UpdateStrongestBella();

        }

        unitScript = GetComponent<Unit>();
        attackInterval = 1f / attackSpeed;
       
       
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            // Find the closest target that is attackable
            GameObject target = FindClosestMonster();
            if (target != null && Time.time >= nextAttackTime)
            {
                FireProjectile(target);
                TryExecute(target);
                nextAttackTime = Time.time + attackInterval;
            }
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
    void FireProjectile(GameObject target)
    {
        // Instantiate the projectile and set its direction towards the target
        GameObject bellaProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        BellaProjectile projectileScript = bellaProjectile.GetComponent<BellaProjectile>();
        bellaProjectile.GetComponent<BellaProjectile>().target = target;
        bellaProjectile.GetComponent<BellaProjectile>().damage = attackDamage;

    }

    void TryExecute(GameObject target)
    {
        Unit unitScript = GetComponent<Unit>();
        MonsterController monsterscript = target.GetComponent<MonsterController>();



        // Execute logic based on the unit's level
        if (unitScript.unitLevel == 1)
        {
            Debug.Log("BellaLevel" + unitScript.unitLevel);
            float ExecuteHpRate = 0.3f;
            monsterscript.Execute(ExecuteHpRate);

        }
        else if (unitScript.unitLevel == 3)
        {
            float ExecuteHpRate = 0.3f;
            if (Random.value <= 0.05f)
                monsterscript.Execute(1.0f);

            else
                monsterscript.Execute(ExecuteHpRate);

        }
        else if (unitScript.unitLevel == 5)
        {
            float ExecuteHpRate = 0.45f;
            monsterscript.Execute(ExecuteHpRate);
        }
    }

    public void ResetAttackDamageToBase()
    {
        attackDamage = 7.0f;
        Debug.Log("Rollback" + attackDamage);
    }

    public void IncreaseAttackDamage(float Times)
    {
        attackDamage = attackDamage * Times;
        Debug.Log("Prestige Apply" + attackDamage);
    }
}
