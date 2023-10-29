using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed ;       // Speed of the projectile
    public GameObject target;     // Target monster
    public float damage = 12f;    // Damage dealt by the projectile
    public float slowAmount = 0.4f;   // Slow percentage
    public float slowDuration = 1f;   // Duration of the slow effect

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
        //MonsterController monsterScript = target.GetComponent<MonsterController>();
        //monsterScript.TakeDamage(damage);
        IDamageable damageableEntity = target.GetComponent<IDamageable>();
        if (damageableEntity != null)
        {
            damageableEntity.TakeDamage(damage);
        }
        Destroy(gameObject);  // Destroy the projectile after hitting the target
    }
}
