using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeresAttack : MonoBehaviour
{
    public float attackDamage = 19f;
    private float currentAttackDamage;
    public float attackSpeed = 2.0f;
    public GameObject projectilePrefab;  // Reference to the projectile sprite prefab
    public GameObject chargedProjectilePrefab;
    /*public float attackRange = 50f; */   
    private float attackInterval;
    private float nextAttackTime = 0f;
    private Unit unitScript;
    private int maxEmbersStacks = 5;  
    private int currentEmberStacks=0;

    // Start is called before the first frame update
    void Start()
    {
        attackInterval = 1f / attackSpeed;

        unitScript = GetComponent<Unit>();

        currentAttackDamage = attackDamage;
    }



    // Update is called once per frame
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
        GameObject projectileToUse = projectilePrefab; // 기본 프로젝타일로 초기화
        Unit unitScript = GetComponent<Unit>();

       

        if (unitScript.unitLevel >= 3 && currentEmberStacks >= 1 && Random.Range(0f, 1f) <= 0.3f)
        {
            
            projectileToUse = chargedProjectilePrefab;
            ChargedAttack();
            Debug.Log("차지어택");
        }

        // 프로젝타일 생성
        GameObject terresProjectile = Instantiate(projectileToUse, transform.position, Quaternion.identity);
        TerresProjectile projectileScript = terresProjectile.GetComponent<TerresProjectile>();
        projectileScript.target = target;
        projectileScript.damage = currentAttackDamage;

        // 레벨에 따른 추가 데미지 계산
        if (unitScript.unitLevel >= 1 && unitScript.unitLevel != 5)
        {
            if (Random.Range(0f, 1f) <= 0.05f)
            {
                if (currentEmberStacks <= maxEmbersStacks)
                {
                    currentEmberStacks++;
                    Debug.Log("현재불씨" + currentEmberStacks);
                }
            }

            currentAttackDamage = (attackDamage + 0.1f * (currentEmberStacks * attackDamage));
        }

        if (unitScript.unitLevel == 5)
        {
            if (currentEmberStacks <= maxEmbersStacks)
            {
                currentEmberStacks++;
                Debug.Log("현재불씨" + currentEmberStacks);
            }

            currentAttackDamage = (attackDamage + 0.1f * (currentEmberStacks * attackDamage));
        }
    }

   void ChargedAttack() //강공격 매서드
    {
        if (currentEmberStacks > 0)
        {
            --currentEmberStacks;
        }

        //currentAttackDamage * 1.3 의 피해
        //강공격 애니메이션
}



}
