using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellaProjectile : MonoBehaviour
{
    public float speed;       // Speed of the projectile
    public GameObject target;     // Target monster
    public float damage = 7;    // Damage dealt by the projectile


    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);  // Destroy the projectile if the target is null
            return;
        }

        // Move the projectile towards the target
        Vector2 moveDirection = (target.transform.position - transform.position).normalized;
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;

        // Check for collision with the target
        if (Vector2.Distance(transform.position, target.transform.position) <= 0.1f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {

        IDamageable damageableEntity = target.GetComponent<IDamageable>();
        if (damageableEntity != null)
        {
            damageableEntity.TakeDamage(damage);
        }
        Destroy(gameObject);  // Destroy the projectile after hitting the target
    }
}
