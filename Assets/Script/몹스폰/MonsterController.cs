using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float speed = 5f;

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

    public void DecreaseSpeed(float reductionAmount)
    {
        speed -= reductionAmount;
        if (speed < 1f) speed = 1f; // 속도가 특정 임계값 아래로 내려가지 않도록 합니다.
    }
}
