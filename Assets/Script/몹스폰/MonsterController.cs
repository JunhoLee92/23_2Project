using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MonsterType { Basic, Enhanced }

public class MonsterController : MonoBehaviour
{
    public MonsterType monsterType;
    public float Hp;
    public float speed;
    private float originalSpeed;

    private void Start()
    {
        originalSpeed = speed;
    }
    void Update()
    {
        MoveTowardsCenter();

        if (Vector2.Distance(transform.position, Vector2.zero) < 0.1f)
        {
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
        if (slowAmount >= 1f)
        {
            speed = 0f;
            this.GetComponent<SpriteRenderer>().color = Color.red; // For Check 
            Debug.Log("Frozen");
        }
        else
        {
            speed *= (1f - slowAmount);
        }
        yield return new WaitForSeconds(duration);
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
