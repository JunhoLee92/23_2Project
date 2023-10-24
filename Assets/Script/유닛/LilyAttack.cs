using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyAttack : MonoBehaviour
{
    public float attackDamage = 17f;
    public float attackSpeed = 1.8f; // 초당 공격 횟수
    private float attackInterval; // 공격 간격 (초)
    private float lastAttackTime; // 마지막 공격 시간
    private float nextAttackTime = 0f;
    public GameObject projectilePrefab; // 발사체 프리팹
    private Unit unitScript;
    private float poisonStack;
    private float poisonDamagePercentage;

    private void Start()
    {
        attackInterval = 1f / attackSpeed;

        unitScript = GetComponent<Unit>();

        // Add the following debug lines
        if (unitScript == null)
        {
            Debug.LogError("Unit script is not attached!");
        }
        else
        {
            Debug.Log("Unit script is attached. UnitType: " + unitScript.unitType);
        }
    }

    private void Update()
    {
        GameObject target = FindClosestMonster();

        if (target && Time.time >= nextAttackTime)
        {
            Attack(target);
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
    private void Attack(GameObject target)
    {
        GameObject Lilyprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        LilyProjectile projectileScript = Lilyprojectile.GetComponent<LilyProjectile>();

        projectileScript.originatingUnitTransform = this.transform;  // 레이저에 생성 유닛의 Transform 값을 전달

        projectileScript.target = target;
        projectileScript.damage = attackDamage;

        if (unitScript && unitScript.unitType == 2)  // Checking if it's Lily
        {
            Debug.Log("Unit Type: " + unitScript.unitType);
            Debug.Log("Unit Level: " + unitScript.unitLevel);



            if (unitScript.unitLevel == 1)
            {
                poisonDamagePercentage = 0.15f;

            }
            else if (unitScript.unitLevel == 3)
            {
                poisonDamagePercentage = 0.25f;
            }

            else if (unitScript.unitLevel == 5)
            {
                poisonDamagePercentage = 0.30f;
                poisonStack = 5;
            }
            // 발사체 생성 및 발사
           
            // TODO: 발사체 방향 및 속도 설정
        }
    }
}
