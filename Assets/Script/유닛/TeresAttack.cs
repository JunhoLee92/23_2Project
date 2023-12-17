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
    private float attackRange = 7f;   
    private float attackInterval;
    private float nextAttackTime = 0f;
    private Unit unitScript;
    private int maxEmbersStacks = 5;  
    private int currentEmberStacks=0;
    private float prestigeBonus = 0; 

    

    private float emberStackProb=0.05f;

    private float ChargedAttackChance;
    
    public static bool isSpecialA=false;

    public static bool isSpecialB=false;

    bool isBoolA=false;
    bool isBoolB=false;

    // Start is called before the first frame update
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

        currentAttackDamage = attackDamage;

        if (GameManager.Instance.unitEvolutionData[4].isPrestige==true)
        {
            prestigeBonus = 0.2f;
            Debug.Log("ChargedAttackBonus" + prestigeBonus);
        }

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
    }



    // Update is called once per frame
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
    {
        GameObject projectileToUse = projectilePrefab; 
        Unit unitScript = GetComponent<Unit>();

      if(!isBoolA&&isSpecialA&&unitScript.unitLevel>=3)
      {
        Debug.Log("SpecialA");
        SpecialA();
        isBoolA=true;
      }

       if(!isBoolB&&isSpecialB&&unitScript.unitLevel==5)
      {
        Debug.Log("SpecialB");
        SpecialB();
        isBoolA=true;
      }

       Debug.Log("emberStack"+emberStackProb);

        if (unitScript.unitLevel >= 3 && currentEmberStacks >= 1 && isBoolA && Random.Range(0f, 1f) <= ChargedAttackChance+prestigeBonus)
        {
            
            projectileToUse = chargedProjectilePrefab;
            ChargedAttack();
            
        }

       
        GameObject terresProjectile = Instantiate(projectileToUse, transform.position, Quaternion.identity);
        TerresProjectile projectileScript = terresProjectile.GetComponent<TerresProjectile>();
        projectileScript.target = target;
        projectileScript.damage = currentAttackDamage;

        
        
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

   void ChargedAttack() 
    {
        if (currentEmberStacks > 0)
        {
            --currentEmberStacks;
        }

        
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
public void SpecialA()
{
   ChargedAttackChance=0.3f;
}
public void SpecialB()
{
   
        emberStackProb+=0.05f;
   
    

}


}
