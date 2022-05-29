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
    public Transform target;

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
                Spell spellInfo = player.spellContext.spells[i];
                if (player.playerStats.TryDecreaseManaLevel(spellInfo.cost, spellInfo.duration))
                {
                    CastSpell(spellInfo);
                    SetReloadTimer(spellInfo);
                    break;
                }

                // Bit shift the index of the layer (8) to get a bit mask
                int layerMask = 1 << 8;

                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, spellInfo.distance, layerMask))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                    Debug.Log("Did not Hit");
                }
            }
        }
    }

    private void SetReloadTimer(Spell spellInfo)
    {
        spellDuration = Observable.Timer(TimeSpan.FromSeconds(spellInfo.duration + spellInfo.delay))
            .TakeUntilDisable(gameObject).Subscribe(_ =>
            {
                spell.Stop();
                spellDuration?.Dispose();
                spellDuration = null;
            });
    }

    private void CastSpell(Spell spellInfo)
    {
        spellDelay = Observable.Timer(TimeSpan.FromSeconds(spellInfo.delay)).Subscribe(_ =>
        {
            spell = Instantiate(spellInfo.prefab).GetComponent<VisualEffect>();
            spell.transform.parent = spellInfo.target == Target.Enemy ? target : spellInfo.target == Target.Player ? transform : null;
            spell.transform.localPosition = spellInfo.localPosition;
            spell.transform.localRotation = spellInfo.localRotation;
            spellDelay.Dispose();
            spellDelay = null;
        });
    }
}
