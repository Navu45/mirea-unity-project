using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class UnitStats : MonoBehaviour
{
    public float health = 100f;

    protected IDisposable waitForRegeneration;

    public virtual float Health
    {
        get => health; set {
            health = Mathf.Clamp(value, 0.0f, 100.0f);
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

    protected virtual void Regenerate() { }
}
