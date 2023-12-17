using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellaAttack : MonoBehaviour
{
    public float attackDamage ;
    public float attackSpeed = 1.0f;
    public GameObject projectilePrefab;
    private Unit unitScript;  

    float ExecuteHpRate = 0.3f;
    float UnconditionalExecutionRate;
    private float attackRange = 10f;
    private float attackInterval;
    private float nextAttackTime = 0f;
    private BellaPrestige bellapretige;

    

  public static bool isSpecialA=false;

   public static bool isSpecialB=false;
    

    bool isBoolA=false;
     bool isBoolB=false;

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

         unitScript = GetComponent<Unit>();
        attackDamage=unitScript.attackPower;

          if (unitScript != null)
        {
            unitScript.OnAttackDamageChanged += UpdateDamage;
            unitScript.OnAttackSpeedChanged += UpdateSpeed;
            attackDamage = unitScript.AttackPower; // Initialize with current attack damage
            attackSpeed =unitScript.AttackSpeed;
            Debug.Log("Subscribed to OnAttackDamageChanged");
        }
        attackInterval = 1f / attackSpeed;

        if (GameManager.Instance.unitEvolutionData[5].isPrestige == true)
        {
            Debug.Log("BellaPrestige");
            bellapretige.UpdateStrongestBella();

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

    void Update()
    {   attackInterval = 1f / attackSpeed;
        if (Time.time >= nextAttackTime)
        {
            // Find the closest target that is attackable
            GameObject target = FindClosestMonster();
            if (target != null && Time.time >= nextAttackTime)
            {
                FireProjectile(target);
                
                nextAttackTime = Time.time + attackInterval;

                //MonsterController monster = target.GetComponent<MonsterController>();

                if (LayerMask.LayerToName(target.layer) != "Boss")
                {
                    TryExecute(target);

                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            isSpecialB=true;
            // SpecialB();
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

        
        MonsterController monsterscript = target.GetComponent<MonsterController>();
        //if(monsterscript != null)
        //{
        //    return;
        //}
        
        
        Debug.Log("Executerate"+ExecuteHpRate);
       
         
            
          
            
            if (Random.value <= UnconditionalExecutionRate)
                monsterscript.Execute(1.0f);

            else
                monsterscript.Execute(ExecuteHpRate);

       
        // else if (unitScript.unitLevel == 5)
        // {
        //     ExecuteHpRate = 0.45f;
        //     monsterscript.Execute(ExecuteHpRate);
        // }
    }

    
private void UpdateDamage(float newDamage)
    {
        attackDamage = newDamage;
        // Additional logic to handle damage change
        Debug.Log("UPdateDamageKali"+attackDamage);

        bellapretige.UpdateStrongestBella();
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

    public void SpecialA()
    {
        UnconditionalExecutionRate=0.05f;
    }
    public void SpecialB()
    {
        ExecuteHpRate+=0.15f;
         
    }
}
