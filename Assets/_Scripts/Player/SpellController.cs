using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public SpellContext spellContext;
    private Player player;
    private GameObject spell;
    public Transform target;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (player.spellCasted[i] && !spell)
            {
                spell = Instantiate(spellContext.spells[i].prefab);
                spell.transform.parent = target;
                spell.transform.position = target.position;
                Destroy(spell, spellContext.spells[i].delayTime);
                break;
            }
        }            
    }


}
