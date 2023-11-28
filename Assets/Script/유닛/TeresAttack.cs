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
    private float prestigeBonus = 0; 

    private float emberStackProb=0.05f;
     bool isSpecialA=false;
    bool isSpecialB=false;

    private static bool globalSpecialAApplied = false;
    private static bool globalSpecialBApplied = false;

    // Start is called before the first frame update
    void Start()
    {
        attackInterval = 1f / attackSpeed;

        unitScript = GetComponent<Unit>();

        currentAttackDamage = attackDamage;

        if (GameManager.Instance.unitEvolutionData[4].isPrestige==true)
        {
            prestigeBonus = 0.2f;
            Debug.Log("ChargedAttackBonus" + prestigeBonus);
        }

        if(globalSpecialBApplied)
        {
            emberStackProb+=0.05f;
        }
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

        if(Input.GetKeyDown(KeyCode.Q))
        {
            isSpecialB=true;
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
        GameObject projectileToUse = projectilePrefab; 
        Unit unitScript = GetComponent<Unit>();

      if(isSpecialB)
        {
         SpecialB();
         isSpecialB=false;
         Debug.Log("Special Applied");
        }

       Debug.Log("emberStack"+emberStackProb);

        if (unitScript.unitLevel >= 3 && currentEmberStacks >= 1 && Random.Range(0f, 1f) <= 0.3f+prestigeBonus)
        {
            
            projectileToUse = chargedProjectilePrefab;
            ChargedAttack();
            
        }

       
        GameObject terresProjectile = Instantiate(projectileToUse, transform.position, Quaternion.identity);
        TerresProjectile projectileScript = terresProjectile.GetComponent<TerresProjectile>();
        projectileScript.target = target;
        projectileScript.damage = currentAttackDamage;

        
        if (unitScript.unitLevel >= 1 && unitScript.unitLevel != 5)
        {
            if (Random.Range(0f, 1f) <= emberStackProb)
            {
                if (currentEmberStacks <= maxEmbersStacks)
                {
                    currentEmberStacks++;
                    Debug.Log("EmberCharged");
                    
                }
            }

            currentAttackDamage = (attackDamage + 0.1f * (currentEmberStacks * attackDamage));
        }

        if (unitScript.unitLevel == 5)
        {   if(Random.Range(0f,1f)<=emberStackProb+0.05f)
            {
                if (currentEmberStacks <= maxEmbersStacks)
                {
                currentEmberStacks++;
              
                }
            }

            currentAttackDamage = (attackDamage + 0.1f * (currentEmberStacks * attackDamage));
        }
    }

   void ChargedAttack() 
    {
        if (currentEmberStacks > 0)
        {
            --currentEmberStacks;
        }

        
    }
public void SpecialA()
{
    //Check
}
public void SpecialB()
{
    if(!globalSpecialBApplied)
    {
        emberStackProb+=0.05f;
        Debug.Log("emberstackprob"+ emberStackProb);
        globalSpecialBApplied=true;
    }
    

}


}
