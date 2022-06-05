using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerStats : UnitStats
{
    [SerializeField] private float mana = 100;

    [SerializeField] private float RecoverySpeed;

    public HealthBar healthBar;
    public ManaBar manaBar;

    public override float Health
    {
        get => base.Health; 
        set {
            base.Health = value;
            UpdateHealthBar();
        }
    }

    public float Mana
    {
        get => mana; set
        {
            mana = Mathf.Clamp(value, 0, 100);
            UpdateManaBar();
            if (waitForRegeneration != null)
            {
               waitForRegeneration.Dispose();
               waitForRegeneration = null;
            }
            waitForRegeneration = Observable.Timer(TimeSpan.FromSeconds(3))
                   .Finally(() => waitForRegeneration = null)
                   .Subscribe(_ => Regenerate());
        }
    }

    protected override void Regenerate()
    {
        Observable.Interval(TimeSpan.FromSeconds(.1f)).TakeWhile(_ => Mana != 100 || Health != 100).Subscribe(_ =>
        {
            if(Mana != 100)
            {
                Mana += RecoverySpeed;
                UpdateManaBar();
            }

            if (Health != 100)
            {
                Health += RecoverySpeed;
                UpdateHealthBar();
            }
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
