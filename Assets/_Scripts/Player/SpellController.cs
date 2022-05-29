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
                if (player.playerStats.TryDecreaseManaLevel(spellInfo.cost, spellInfo.duration))
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
                Debug.Log("Here1");
                if (spell)
                {
                    spell.Stop();
                    Observable.Timer(TimeSpan.FromSeconds(spellInfo.destroyDelay)).Subscribe(_ =>
                    {
                        Destroy(spell.gameObject);
                        spellDuration?.Dispose();
                        spellDuration = null;
                    });
                }                
            });
    }

    private void CastSpell(Spell spellInfo)
    {
        if (spellInfo.target != Target.Player && !player.TryGetTarget(spellInfo))
        {
            Debug.Log("Here");
            return;
        }

        spellDelay = Observable.Timer(TimeSpan.FromSeconds(spellInfo.startDelay)).Subscribe(_ =>
        {
            spell = Instantiate(spellInfo.prefab).GetComponent<VisualEffect>();
            spell.transform.parent = spellInfo.target == Target.Enemy ? player.target.transform : spellInfo.target == Target.Player ? transform : null;
            spell.transform.localPosition = spellInfo.localPosition;
            spell.transform.localRotation = spellInfo.localRotation;
            spellDelay?.Dispose();
            spellDelay = null;
        });
    }
}
