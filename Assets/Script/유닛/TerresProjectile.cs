using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerresProjectile : MonoBehaviour
{
    public float speed=10;       // Speed of the projectile
    public GameObject target;     // Target monster
    public float damage;    // Damage dealt by the projectile
    public bool isCharged = false;
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);  // Destroy the projectile if the target is null
            return;
        }
       
        Vector3 direction = (target.transform.position - transform.position).normalized;

        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Move the projectile towards the target
        Vector2 moveDirection = (target.transform.position - transform.position).normalized;
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;

        // Check for collision with the target
        if (Vector2.Distance(transform.position, target.transform.position) <= 0.1f)
        {
            if (isCharged == false)
            {
                HitTarget();
            }
            else if (isCharged==true)
            {
                ChargedTarget();
            }
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

    void ChargedTarget()
    {
        IDamageable damageableEntity = target.GetComponent<IDamageable>();
        if (damageableEntity != null)
        {
            damageableEntity.TakeDamage(damage*1.3f);
            Debug.Log("ChargedAttack");
        }
        Destroy(gameObject);
    }
}
