using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpellContext", fileName = "NewSpells", order = 51)]
public class SpellContext : ScriptableObject
{
    public List<Spell> spells;
}

[System.Serializable]
public struct Spell
{
    public GameObject prefab;
    public float cost;
    public float damage;
    public float startDelay;
    public float duration;
    public float destroyDelay;
    public bool continuous;
    public float distance;
    public Target target;
    public Target spawnPoint;
    public Vector3 localPosition;
    public Quaternion localRotation;

    public float ReloadTime => startDelay + duration + destroyDelay;
}
public enum Target
{
    Player,
    Enemy,
    None
}
