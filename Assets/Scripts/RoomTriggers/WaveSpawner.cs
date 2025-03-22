using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Room Triggers/Wave Spawner")]
public class WaveSpawner : ScriptableObject
{
    
    [Tooltip("Spawn 'amount' random 'enemies' each 'overtime', and then wait x 'cooldown' before next group spawns")]
    public EnemyGroup[] groups;

}

[System.Serializable]
public class EnemyGroup
{
    public GameObject[] enemies;
    public int amount;
    public int overTime;
    public int cooldownn;
}
