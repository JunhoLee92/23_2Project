using System.Collections;
using UnityEngine;
public enum MonsterType { Basic, Enhanced }

public class MonsterController : MonoBehaviour
{
    public MonsterType monsterType;
    public float Hp;
    public float speed;
    private float originalSpeed;
    public GameObject slowEffectPrefab;  // 슬로우 애니메이션 프리팹을 참조하는 변수
    private GameObject currentSlowEffect;  

    private void Start()
    {
        originalSpeed = speed;
    }
    void Update()
    {
        MoveTowardsCenter();

        if (Vector2.Distance(transform.position, Vector2.zero) < 0.1f)
        {
            BaseHealth baseHealth = FindObjectOfType<BaseHealth>();
            if (baseHealth != null)
            {
                baseHealth.TakeDamage(10);  // 예: 본진에 10의 데미지를 입힘
            }
            Destroy(gameObject);
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
        //슬로우 애니 파괴
        if (currentSlowEffect != null)
        {
            Destroy(currentSlowEffect);
        }

        speed = originalSpeed;
    }
    //public void DecreaseSpeed(float reductionAmount)
    //{
    //    speed -= reductionAmount;
    //    if (speed < 1f) speed = 1f; // 속도가 특정 임계값 아래로 내려가지 않도록 합니다.
    //}

    public void TakeDamage(float damage)
    {
        Hp -= damage;  // Subtract damage from HP
        Debug.Log(Hp);
        // Check if the monster's HP is zero or below
        if (Hp <= 0f)
        {
            Die();  // If yes, trigger the Die method
        }
    }

    void Die()
    {
        // Here you can add logic for what happens when the monster dies,
        // such as playing a death animation, adding score, etc.
        Destroy(gameObject);  // For now, simply destroy the monster game object
    }

}
