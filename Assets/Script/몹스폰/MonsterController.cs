using System.Collections;
using UnityEngine;
public enum MonsterType { Basic, Enhanced}

public class MonsterController : MonoBehaviour, IDamageable
{
    public MonsterType monsterType;
    public float Hp;
    public float MaxHp;
    public float speed;
    private float originalSpeed;
    public GameObject slowEffectPrefab;  // 
    private GameObject currentSlowEffect;
    public GameObject executeEffectPrefab;
    private GameObject currentExecuteEffect;
    private int poisonStacks = 0;
    public int maxPoisonStacks = 3;
    private float PoisonDamage = 0;
    private bool isDead = false;
    GameManager gameManager;
    private void Start()
    {
        speed *= RoundRewardSystem.globalSpeedModifier;
        originalSpeed = speed;
        
        gameManager = GameManager.Instance;
        MaxHp = Hp;
    }
    void Update()
    {
        MoveTowardsCenter();

        if (Vector2.Distance(transform.position, Vector2.zero) < 0.1f)
        {
            BaseHealth baseHealth = FindObjectOfType<BaseHealth>();
            if (baseHealth != null)
            {
                baseHealth.TakeDamage(10);

                if (isDead == false)
                {
                    isDead = true;
                    Die();// 
                }
            }
            
        }
    }

    void MoveTowardsCenter()
    {
        Vector2 direction = (Vector2.zero - (Vector2)transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;

    }
    public void Slow(float slowAmount, float duration)
    {
        Debug.Log("slow");
        StartCoroutine(ApplySlow(slowAmount, duration));
    }

    IEnumerator ApplySlow(float slowAmount, float duration)
    {


        speed *= (1f - slowAmount);

        currentSlowEffect = Instantiate(slowEffectPrefab, transform.position, Quaternion.identity, transform);

        Debug.Log("Frozen");
        yield return new WaitForSeconds(duration);
        //Slow Anim Destroy
        if (currentSlowEffect != null)
        {
            Destroy(currentSlowEffect);
        }

        speed = originalSpeed;
    }

    public void Execute(float ExecuteHpRate)
    {
        
        float Hprate = Hp / MaxHp;
        
       
        if (Hprate <= ExecuteHpRate)
        {
            Debug.Log("Execute");
            StartCoroutine(ApplyExecute());
            
        }
       
        
    }


    IEnumerator ApplyExecute()
    {

        currentExecuteEffect = Instantiate(executeEffectPrefab, transform.position, Quaternion.identity, transform);

        Debug.Log("Execute");
        yield return new WaitForSeconds(0.3f);
        
        if (currentExecuteEffect != null)
        {
            Destroy(currentExecuteEffect);
        }

        if (isDead == false)
        {
            isDead = true;
            Die();
        }

        
    }
    //public void DecreaseSpeed(float reductionAmount)
    //{
    //    speed -= reductionAmount;
    //    if (speed < 1f) speed = 1f; // 
    //}

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        if (this == null || gameObject == null)
            return;

        Hp -= damage;  // Subtract damage from HP
        Debug.Log(Hp);
        // Check if the monster's HP is zero or below
        if (Hp <= 0f && !isDead )
        {  isDead = true;
            Die();  // If yes, trigger the Die method
        }
    }
    public int MaxPoisonStacks
    {
        get { return maxPoisonStacks; }
        set { maxPoisonStacks = value; }
    }

    public void PoisonStack(float Damage)
    {
        poisonStacks = Mathf.Min(poisonStacks + 1, maxPoisonStacks);  // Ensure poison stacks don't exceed maxPoisonStacks
        PoisonDamage = Damage;
        if (poisonStacks == 1)  // If it's the first stack, start the poison routine
        {
            StartCoroutine(PoisonDamageRoutine());
        }
    }

    public void EnhancedPoisonStack(float Percentage,float Damage) //If Lily isPrestige
    {
        poisonStacks = Mathf.Min(poisonStacks + 1, maxPoisonStacks); 
        PoisonDamage = Damage;
        if (poisonStacks == 1)  
        {
            StartCoroutine(EnhancedPoisonDamageRoutine(Percentage,Damage));
            
        }
    }
    private IEnumerator EnhancedPoisonDamageRoutine(float Percentage, float Damage)
    {
        int IncreaseCount = 0;
        while (poisonStacks > 0)
        {
            yield return new WaitForSeconds(1.0f); // Every 1 second
             if(IncreaseCount<=10)
            {
                Percentage += 0.01f;
                IncreaseCount++;
                Debug.Log("Enhanced");

            }
            for (int i = 0; i < poisonStacks; i++)
            {
                TakeDamage(Percentage*Damage);
                Debug.Log("poisonDamage" +Percentage*Damage);
            }
        }
    }

    private IEnumerator PoisonDamageRoutine()
    {
        while (poisonStacks > 0)
        {
            yield return new WaitForSeconds(1.0f); // Every 1 second
            
            for (int i = 0; i < poisonStacks; i++)
            {
                TakeDamage(PoisonDamage);
                Debug.Log("poisonDamage" + PoisonDamage);
            }
        }
    }

    void Die()
    {

        
        if (this.gameObject != null)
        {
            gameManager.OnMonsterDestroyed();
            Destroy(gameObject); 

        }
        else
        {
            return;
        }

    }
}
