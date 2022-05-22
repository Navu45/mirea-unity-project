using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellContext : ScriptableObject
{
    public List<Spell> spellPrefabs;
}

[System.Serializable]
public class Spell
{
    public GameObject prefab;
    public bool ableToCastRunning;
}
