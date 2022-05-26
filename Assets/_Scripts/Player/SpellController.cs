using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.VFX;

public class SpellController : MonoBehaviour
{
    public SpellContext spellContext;
    private Player player;
    private VisualEffect spell;
    private IDisposable spellDuration;
    public Transform target;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (player.spellCasted[i] && spellDuration == null)
            {
                Spell spellInfo = spellContext.spells[i];
                if (player.playerStats.TryDecreaseManaLevel(spellInfo.cost, spellInfo.duration))
                {
                    spell = Instantiate(spellInfo.prefab).GetComponent<VisualEffect>();
                    CastSpell(spellInfo);
                    SetReloadTimer(spellInfo);
                    break;
                }                
            }
        }
    }

    private void SetReloadTimer(Spell spellInfo)
    {
        if (spellDuration == null)
        {
            spellDuration = Observable.Timer(TimeSpan.FromSeconds(spellInfo.duration))
            .TakeUntilDisable(gameObject).Subscribe(_ =>
            {
                spell.Stop();
                spellDuration.Dispose();
                spellDuration = null;
            });
        }
    }

    private void CastSpell(Spell spellInfo)
    {
        PlaceVFX(spellInfo);
    }

    private void PlaceVFX(Spell spellInfo)
    {
        spell.transform.parent = spellInfo.target == Target.Enemy ? target : spellInfo.target == Target.Player ? transform : null;
        spell.transform.localPosition = spellInfo.localPosition;
        spell.transform.localRotation = spellInfo.localRotation;
    }
}
