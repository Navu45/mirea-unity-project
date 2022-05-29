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
    public int cost;
    public Target target;
    public Target spawnPoint;
    public float delay;
    public float duration;
    public float distance;
    public Vector3 localPosition;
    public Quaternion localRotation;
}
public enum Target
{
    Player,
    Enemy,
    None
}
