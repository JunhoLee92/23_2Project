using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
    void PoisonStack(float damage);

    int MaxPoisonStacks { get; set; }
}
