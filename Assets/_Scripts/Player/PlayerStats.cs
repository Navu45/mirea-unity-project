using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [field: SerializeField] public int health { get; private set; } = 100;
    [field: SerializeField] public int mana { get; private set; } = 100;

    IDisposable manaConsume;

    public bool TryDecreaseManaLevel(int cost, float duration)
    {
        if (mana - cost < 0)
        {
            return false;
        }

        manaConsume = Observable.Interval(TimeSpan.FromSeconds(1)).Timeout(TimeSpan.FromSeconds(duration)).Subscribe(m =>
        {            
            mana -= cost;
        });
        return true;
    }

    //[field: SerializeField] public ProgressBar mana { get; }    
    //[field: SerializeField] public ProgressBar mana { get; }
}
