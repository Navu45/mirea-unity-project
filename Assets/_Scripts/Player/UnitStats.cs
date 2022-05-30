using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UnitStats : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int Health { get; private set; } = 100;

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    //[field: SerializeField] public ProgressBar mana { get; }    
    //[field: SerializeField] public ProgressBar mana { get; }
}
