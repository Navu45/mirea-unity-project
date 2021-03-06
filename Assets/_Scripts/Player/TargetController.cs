using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Enemy[] enemies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.currentBattlefield = this;
            
            foreach (Enemy enemy in enemies)
            {
                enemy.SetTarget(player);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (player.currentBattlefield == this)
            {
                player.currentBattlefield = null;
            }
        }
    }

    public Enemy GetRandomEnemy()
    {
        return enemies[Random.Range(0, enemies.Length)];
    }
}
