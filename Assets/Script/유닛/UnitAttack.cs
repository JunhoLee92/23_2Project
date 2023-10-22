using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{

    public float attackDamage = 21f;       // Attack power
    public float attackRange = 1f;         // Attack range (1 block)
    public float attackSpeed = 2.2f;       // Attacks per second
    public GameObject attackEffectPrefab;  // Reference to the attack effect sprite prefab
    public float effectDuration = 0.5f;    // Duration for which the effect sprite is visible

    private float attackInterval;          // Time between attacks
    private float nextAttackTime = 0f;     // Time when next attack can occur

    void Start()
    {

        attackInterval = 1f / attackSpeed;  // Calculate the time between attacks
    }

    void Update()
    {
        // Check if it's time to attack
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackInterval;
        }
    }

    void Attack()
    {
        // Detect monsters within attack range using a simple overlap check
        Collider2D[] hitMonsters = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (var monster in hitMonsters)
        {
            // Assuming the monster has a tag "Monster"
            if (monster.CompareTag("Monster"))
            {
                // Apply damage to the monster
                monster.GetComponent<MonsterController>().TakeDamage(attackDamage);

                // Instantiate the attack effect sprite
                GameObject effectInstance = Instantiate(attackEffectPrefab, this.transform.position, Quaternion.identity);

                // Destroy the effect sprite after the set duration
                Destroy(effectInstance, effectDuration);
            }
        }
    }
}

