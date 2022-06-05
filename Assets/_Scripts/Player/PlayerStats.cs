using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerStats : UnitStats
{
    [SerializeField] private int mana = 100;

    [SerializeField] private int RecoverySpeed = 5;

    public HealthBar healthBar;
    public ManaBar manaBar;
    private IDisposable waitForRegeneration;

    public int Mana
    {
        get => mana; set
        {
            mana = Mathf.Clamp(value, 0, 100);
            UpdateManaBar();
            if (waitForRegeneration == null)
            {
                waitForRegeneration = Observable.Timer(TimeSpan.FromMilliseconds(10))
                    .Finally(() => waitForRegeneration = null)
                    .Subscribe(_ => Regenerate());
            }
        }
    }

    private void Start()
    {
        OnTakeDamage += UpdateHealthBar;        
    }

    private void Regenerate()
    {
        Observable.Interval(TimeSpan.FromSeconds(.1f)).TakeWhile(_ => Mana != 100 && Health != 100).Subscribe(_ =>
        {
            Mana += RecoverySpeed / 10;
            Health += RecoverySpeed / 10;
        });
    }

    private void UpdateHealthBar()
    {
        healthBar?.SetHealth(Health);
    }
    private void UpdateManaBar()
    {
        manaBar?.SetMana(Mana);
    }
}
