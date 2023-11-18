using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
    void PoisonStack(float damage);

    void EnhancedPoisonStack(float Percentage,float Damage);

    int MaxPoisonStacks { get; set; }
}
