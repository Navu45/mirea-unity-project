using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class UnitStats : MonoBehaviour
{
    [field: SerializeField] public int health { get; private set; } = 100;

    protected UnityAction OnTakeDamage;
    public int Health
    {
        get => health; set {
            health = Mathf.Clamp(value, 0, 100);
            OnTakeDamage?.Invoke();
        }
    }

    //[field: SerializeField] public ProgressBar mana { get; }    
    //[field: SerializeField] public ProgressBar mana { get; }
}
