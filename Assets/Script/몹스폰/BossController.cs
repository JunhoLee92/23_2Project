using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour,IDamageable
{
    

    public int AttackPower;
    public bool IsAlive { get; private set; } = true;
    public float maxHealth;
    public float attackPreparationTime = 30f;
   

    private float currentHealth;
    private int currentPhase = 1;
    private float phaseHealthLimit;  // 각 페이즈에서 체력 한도
    private float attackTimer;
    SpriteRenderer spriteRenderer;
    BossSpawner bossSpawner;
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
            baseHealth.TakeDamage(AttackPower);  // 50은 보스의 공격력을 나타내는 예시 값입니다.
            attackTimer = attackPreparationTime;  // 타이머 리셋
        }

        // 체력이 한도 이하로 떨어졌거나 공격 준비 시간이 지나면 다음 페이즈로 전환
        
    }

    public void TakeDamage(float damage)
    {
        if (this == null || gameObject == null)
            return;
        currentHealth -= damage;
        float healthLost = maxHealth - currentHealth;

        if (healthLost >= 5000 || attackTimer <= 0)
        {
            // 체력 감소에 따른 페이즈 전환 조건 추가
            if (currentPhase == 1 && healthLost >= 5000)
            {
                NextPhase();
            }
            else if (currentPhase == 2 && healthLost >= 10000)
            {
                NextPhase();
            }
            else if (attackTimer <= 0 && currentPhase < 3) // 마지막 페이즈에서는 attackTimer가 0이되어도 NextPhase를 호출하지 않음
            {
                NextPhase();
            }
        }
        Debug.Log("보스현재체력:" + currentHealth);
        // 보스 체력이 0 이하로 떨어졌을 때 처리
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            // 게임 종료 또는 다음 라운드/챕터 로직 추가
        }
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
        }

        switch (currentPhase)
        {
            case 1:break;
            case 2: Debug.Log("Case2");break;
            case 3:Debug.Log("Case3");
                break; ;
        }

        if (currentPhase > 3)
        {
            // 보스를 세 번 격파했을 때 처리
            Destroy(gameObject);
            // 게임 종료 또는 다음 라운드/챕터 로직 추가
            return;
        }

        attackTimer = attackPreparationTime;
        // 다른 페이즈 전환 로직 (예: 애니메이션, 공격 패턴 변경 등)
    }
}
