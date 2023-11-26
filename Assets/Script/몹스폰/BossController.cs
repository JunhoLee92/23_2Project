using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour,IDamageable
{
    

    public int AttackPower;
    public bool IsAlive { get; private set; } = true;
    public float maxHealth;
    public float attackPreparationTime = 5f;
   

    private float currentHealth;
    public float Phase2Hp;
    public float Phase3Hp;
    private int currentPhase = 1;
    private float phaseHealthLimit;  // �� ������� ü�� �ѵ�
    private float attackTimer;
    SpriteRenderer spriteRenderer;
    BossSpawner bossSpawner;
    private int poisonStacks = 0;
    public int maxPoisonStacks = 3;
    private float PoisonDamage = 0;

    public GameObject victory;
    private void Start()
    {
        bossSpawner = GetComponent<BossSpawner>();
        currentHealth = maxHealth;
        phaseHealthLimit = maxHealth/3;
        attackTimer = attackPreparationTime;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            BaseHealth baseHealth = FindObjectOfType<BaseHealth>();
            baseHealth.TakeDamage(AttackPower);  // 50�� ������ ���ݷ��� ��Ÿ���� ���� ���Դϴ�.
            NextPhase();
            attackTimer = attackPreparationTime;  // Ÿ�̸� ����
        }

        // ü���� �ѵ� ���Ϸ� �������ų� ���� �غ� �ð��� ������ ���� ������� ��ȯ
        
    }

    public void TakeDamage(float damage)
    {
        if (this == null || gameObject == null)
            return;
        currentHealth -= damage;
      

        if (currentHealth<=0 || attackTimer <= 0)
        {
            // ü�� ���ҿ� ���� ������ ��ȯ ���� �߰�
            if (currentPhase == 1 )
            {
                NextPhase();
                currentHealth = Phase2Hp;
            }
            else if (currentPhase == 2)
            {
                NextPhase();
                currentHealth = Phase3Hp;
            }
            else if (currentPhase==3) // ������ ��������� attackTimer�� 0�̵Ǿ NextPhase�� ȣ������ ����
            {
                NextPhase();
            }
        }
        //Debug.Log("��������ü��:" + currentHealth);
        
    }

    private void NextPhase()
    {
        bossSpawner = GetComponent<BossSpawner>();
        currentPhase++;
        switch (currentPhase)
        {
            case 1: break;

            case 2:
                Debug.Log("Phase2");
                transform.position = new Vector3(8f,0f,0);
                spriteRenderer.flipX = false; // Flip to face right
                spriteRenderer.flipY = false;
                transform.rotation = Quaternion.Euler(0, 0, 0); // Reset rotation
                break;

            case 3:
                Debug.Log("Phase3");
                transform.position = new Vector3(0f, 4.0f, 0);
                spriteRenderer.flipX = false;
                spriteRenderer.flipY = false;
                transform.rotation = Quaternion.Euler(0, 0, 90); // Rotate to face downward
                break;

            case 4:
                Debug.Log("Phase4");
                break;
        }

       

        if (currentPhase > 3)
        {
            if (currentHealth <= 0)
            {
                GameManager.Instance.Victory();
                Debug.Log("�¸�");
            }
            else {
                GameManager.Instance.Defeat();
                Debug.Log("�й�");
                    }

            // ������ �� �� �������� �� ó��
            Destroy(gameObject);
            // ���� ���� �Ǵ� ���� ����/é�� ���� �߰�
            return;
        }

        attackTimer = attackPreparationTime;
        // �ٸ� ������ ��ȯ ���� (��: �ִϸ��̼�, ���� ���� ���� ��)
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

    public void EnhancedPoisonStack(float Percentage, float Damage) //If Lily isPrestige
    {
        poisonStacks = Mathf.Min(poisonStacks + 1, maxPoisonStacks);
        PoisonDamage = Damage;
        if (poisonStacks == 1)
        {
            StartCoroutine(EnhancedPoisonDamageRoutine(Percentage, Damage));

        }
    }
    private IEnumerator EnhancedPoisonDamageRoutine(float Percentage, float Damage)
    {
        int IncreaseCount = 0;
        while (poisonStacks > 0)
        {
            yield return new WaitForSeconds(1.0f); // Every 1 second
            if (IncreaseCount <= 10)
            {
                Percentage += 0.01f;
                IncreaseCount++;
                Debug.Log("Enhanced");

            }
            for (int i = 0; i < poisonStacks; i++)
            {
                TakeDamage(Percentage * Damage);
                Debug.Log("poisonDamage" + Percentage * Damage);
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

    //private IEnumerator IncreasePoisonDamage() //If Lily's Prestige
    //{
    //    float duration = 10f;
    //    float interval = 1f;

    //    float endTime = Time.time + duration;

    //    while(Time.time <endTime)
    //    {
    //        PoisonDamage += 1.01f * PoisonDamage;
    //        Debug.Log("PoisonIncrease" + PoisonDamage);
    //    }
    //    yield return new WaitForSeconds(interval);
      
    //}

}
