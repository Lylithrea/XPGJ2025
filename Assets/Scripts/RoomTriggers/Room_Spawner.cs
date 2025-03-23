using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Room_Spawner : MonoBehaviour
{
    public WaveSpawner spawner;
    
    public IEnumerator SpawnEnemies()
    {
        foreach (EnemyGroup group in spawner.groups)
        {
            for (int i = 0; i < group.amount; i++)
            {
                GameObject newEnemy = Instantiate(group.enemies[Random.Range(0, group.enemies.Length)]);
                newEnemy.transform.position = this.transform.position;
                newEnemy.GetComponent<EnemyAI>().player = PlayerManager.Instance.gameObject;
                newEnemy.GetComponent<NavMeshAgent>().Warp(this.transform.position);
                yield return new WaitForSeconds(group.overTime);
            }
            yield return new WaitForSeconds(group.cooldownn);
        }
    }
}
