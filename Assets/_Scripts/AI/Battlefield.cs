using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TargetController))]
public class Battlefield : MonoBehaviour
{
    public static int enemyCount;
    public static UnityAction OnAllEnemiesDied;
    public static int EnemyCount
    {
        get => enemyCount; set
        {
            enemyCount = value;
            if (enemyCount == 0)
            {
                OnAllEnemiesDied?.Invoke();
            }
        }
    }

    private void Start()
    {
        enemyCount += GetComponent<TargetController>().enemies.Length;
    }
}
