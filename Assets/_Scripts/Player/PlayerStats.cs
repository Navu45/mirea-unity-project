using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerStats : UnitStats
{
    [field: SerializeField] public int Mana { get; private set; } = 100;

    public bool TryDecreaseManaLevel(int cost, float duration)
    {
        if (Mana - cost < 0)
        {
            return false;
        }

        Observable.Interval(TimeSpan.FromSeconds(1))
            .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(duration)))
            .Subscribe(m =>
            {
                Mana -= cost;
            });

        return true;
    }
}
