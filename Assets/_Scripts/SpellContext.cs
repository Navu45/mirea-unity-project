using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpellContext", fileName = "NewSpells", order = 51)]
public class SpellContext : ScriptableObject
{
    public List<Spell> spells;
}

[System.Serializable]
public class Spell
{
    public GameObject prefab;
    public bool ableToCastRunning;
    public float delayTime;
}
