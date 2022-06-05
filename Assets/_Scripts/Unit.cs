using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Unit : MonoBehaviour
{
    public Unit target;
    [HideInInspector] public UnitStats stats;

    [field: SerializeField]
    public bool NoHP
    {
        get; protected set;
    }

    public void TakeDamage(float damage) {
        stats.Health -= damage;
        NoHP = stats.Health == 0;
    }

    public void MakeDamage(float damage)
    {
        if (target)
        {
            target.TakeDamage(damage);
            target.SetTarget(this);
        }
    }

    public abstract void SetTarget(Unit target);

    protected virtual void Start()
    {
        stats = GetComponent<UnitStats>();
    }
}
