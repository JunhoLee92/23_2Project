using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LilyAttack : MonoBehaviour
{

    //public GameObject laserPrefab;
    //private GameObject currentLaser;
    //private Unit unitScript;
    //public float damage = 17f;
    //public float damageInterval = 0.2f;
    //public float poisonDamagePercentage = 0.05f;
    //private GameObject target;
    //IEnumerator dotDamageCoroutine;
    //private void Start()
    //{
    //    unitScript = GetComponent<Unit>();
    //}
    //void Update()
    //{

    //    target = FindClosestMonster();

    //   if(target != null )
    //    {
    //        StartLaserAttack();

    //    }

    //   if(target == null)
    //    {
    //        Destroy(currentLaser);
    //    }    
    //}

    //GameObject FindClosestMonster()
    //{
    //    GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
    //    GameObject closest = null;
    //    float distance = Mathf.Infinity;

    //    foreach (GameObject monster in monsters)
    //    {
    //        float curDistance = Vector2.Distance(transform.position, monster.transform.position);
    //        if (curDistance < distance /*&& curDistance <= attackRange*/)
    //        {
    //            closest = monster;
    //            distance = curDistance;
    //        }
    //    }
    //    return closest;
    //}
    //IEnumerator DealDotDamage(IDamageable damageableEntity)
    //{
    //    while (damageableEntity != null) // 대상이 존재하고 체력이 0 이상인 경우
    //    {
    //        yield return new WaitForSeconds(damageInterval);
    //        float dotDamage = damage * poisonDamagePercentage;
    //        damageableEntity.TakeDamage(dotDamage);
    //    }
    //    StopLaserAttack();
    //}
    //void StartLaserAttack()
    //{
    //    if (unitScript && unitScript.unitType == 2)  // Checking if it's Lily
    //    {
    //        if (unitScript.unitLevel == 1)
    //        {
    //            poisonDamagePercentage = 0.05f;
    //            damageInterval = 0.2f;
    //        }
    //        else if (unitScript.unitLevel == 3)
    //        {
    //            poisonDamagePercentage = 0.10f;
    //            damageInterval = 0.2f;
    //        }
    //        else if (unitScript.unitLevel == 5)
    //        {
    //            poisonDamagePercentage = 0.10f;
    //            damageInterval = 0.15f;
    //        }
    //    }
    //    //}
    //    if (currentLaser == null)
    //    {
    //        currentLaser = Instantiate(laserPrefab, transform.position, Quaternion.Euler(0, 0, 180), transform);
    //    }

    //    // 레이저에게 몬스터를 향하도록 지시
    //    currentLaser.transform.up = target.transform.position - transform.position;

    //    // 레이저의 길이를 유닛과 몬스터 사이의 거리로 설정
    //    float laserLength = Vector2.Distance(transform.position, target.transform.position);
    //    currentLaser.transform.localScale = new Vector2(1, laserLength / 4.5f);

    //    //// target을 IDamageable로 변환하여 사용
    //    //IDamageable damageableEntity = target.GetComponent<IDamageable>();

    //    //if (damageableEntity != null)
    //    //{
    //    //    dotDamageCoroutine = DealDotDamage(damageableEntity);
    //    //    StartCoroutine(dotDamageCoroutine);
    //    //}

    //    if (dotDamageCoroutine != null)
    //    {
    //        StopCoroutine(dotDamageCoroutine);
    //    }

    //    // 새로운 몬스터에 대한 데미지 코루틴 시작
    //    IDamageable damageableEntity = target.GetComponent<IDamageable>();
    //    if (damageableEntity != null)
    //    {
    //        dotDamageCoroutine = DealDotDamage(damageableEntity);
    //        StartCoroutine(dotDamageCoroutine);
    //    }
    //}



    //void StopLaserAttack()
    //{
    //    if (currentLaser != null)
    //    {
    //        Destroy(currentLaser);
    //    }
    //}


    public GameObject laserPrefab;
    public float AttackDamage = 17f;
    private float poisonDamagePercentage = 0.15f;
    private GameObject target;
    private Coroutine laserAttackCoroutine;
    private Unit unitScript;

    private void Start()
    {
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

    //void ShootLaser(GameObject target)
    //{
       
       
    //        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity,transform);
    //    laser.transform.up = target.transform.position - transform.position;
    //    float laserLength = Vector2.Distance(transform.position, target.transform.position);
    //   laser.transform.localScale = new Vector2(1, laserLength / 4.5f);
    //    // Deal damage to the monster
    //    IDamageable damageableEntity = target.GetComponent<IDamageable>();

    //    if (unitScript && unitScript.unitType == 2)  // Checking if it's Lily
    //    {
    //        if (unitScript.unitLevel == 1)
    //        {
    //            poisonDamagePercentage = 0.05f;

    //        }
    //        else if (unitScript.unitLevel == 3)
    //        {
    //            poisonDamagePercentage = 0.1f;

    //        }
    //        else if (unitScript.unitLevel == 5)
    //        {
    //            poisonDamagePercentage = 0.20f;
    //            damageableEntity.MaxPoisonStacks = 5;
    //        }
    //    }

    //    if (damageableEntity != null)
    //    {
            
    //        damageableEntity.TakeDamage(AttackDamage);
    //        damageableEntity.PoisonStack(poisonDamagePercentage * AttackDamage);
    //    }



       
    //    Destroy(laser, 0.5f);
    //}

    //For Test: Lily Attack Delay 1sec//

    void ShootLaser(GameObject target)
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity, transform);
        laser.transform.up = target.transform.position - transform.position;
        float laserLength = Vector2.Distance(transform.position, target.transform.position);
        laser.transform.localScale = new Vector2(0.5f, laserLength / 4.5f);

        // 데미지를 입히는 부분을 코루틴으로 변경
        StartCoroutine(DealDamageWithDelay(target,0.3f));

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

