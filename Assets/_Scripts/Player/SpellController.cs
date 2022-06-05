using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.VFX;

public class SpellController : MonoBehaviour
{
    private Player player;
    private VisualEffect spell;
    private IDisposable spellDelay;
    private IDisposable spellDuration;
    private Spell spellInfo;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (player.spellCasted[i] && spellDuration == null && spellDelay == null)
            {
                spellInfo = player.spellContext.spells[i];
                if (player.TryGetTarget(spellInfo) && player.TryDecreaseManaLevel(spellInfo.cost, spellInfo.duration))
                {
                    CastSpell(spellInfo);
                    SetReloadTimer(spellInfo);
                    break;
                }
            }
        }
    }

    private void SetReloadTimer(Spell spellInfo)
    {
        spellDuration = Observable.Timer(TimeSpan.FromSeconds(spellInfo.duration + spellInfo.startDelay))
            .TakeUntilDisable(gameObject)
            .Subscribe(_ =>
            {
                spell.Stop();
                Observable.Timer(TimeSpan.FromSeconds(spellInfo.destroyDelay)).Subscribe(_ =>
                {
                    Destroy(spell.gameObject);
                    spellDuration?.Dispose();
                    spellDuration = null;
                });
            });
    }

    private void CastSpell(Spell spellInfo)
    {
        spellDelay = Observable.Timer(TimeSpan.FromSeconds(spellInfo.startDelay))
            .Finally(() =>
            {
                CauseTargetDamage(spellInfo);
            })
            .Subscribe(_ =>
            {
                spell = Instantiate(spellInfo.prefab).GetComponent<VisualEffect>();
                spell.transform.parent = player.target ? player.target.transform : transform;
                spell.transform.localPosition = spellInfo.localPosition;
                spell.transform.localRotation = spellInfo.localRotation;
                spellDelay?.Dispose();
                spellDelay = null;
            });
    }

    private void CauseTargetDamage(Spell spellInfo)
    {
        IObservable<long> observable = null;
        float interval;
        if (spellInfo.continuous)
        {
            observable = Observable.Interval(TimeSpan.FromSeconds(.1f))
                        .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(spellInfo.duration)));
            interval = 10;
        }
        else
        {
            observable = Observable.Timer(TimeSpan.FromSeconds(spellInfo.startDelay));
            interval = 1;
        }
        
        observable.TakeWhile(_ => player.target && !player.target.NoHP).Subscribe(_ =>
        {
            player.MakeDamage(spellInfo.damage / interval);
        });

    }
}
