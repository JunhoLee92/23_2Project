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
}
